using System.Collections;
using JuiceIt2Content.Programming.Enemy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class PlayerEngine : MonoBehaviour
    {
        [SerializeField, Header("Movement")] private int moveSpeed = 500;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float deceleration = 0.3f;
        [SerializeField, Space, Header("AutoShoot")] private float bulletSpawnTime = 1;
        [SerializeField] private float minDistance = 10;
        [SerializeField] private float autoShootRadiusDetection = 15;
        [SerializeField] private GameObject bullet;
        [SerializeField, Space, Header("Ult")]private float maxExplosionRadius = 15;
        [SerializeField] private float propagationSpeed = 5;
        [SerializeField] private float effectDuration = 2;
        [SerializeField] private GameObject[] explosionEffects;
 
        private Rigidbody _rb;
        private Vector2 _moveInputAxis;

        
        
        private float _autoShootTimer;
        
        private static float Score {get; set;}

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MoveAction();
        }

        private void Update()
        {
            AutoShoot();
        }

        #region INPUTS

        public void MoveInput(InputAction.CallbackContext pContext)
        {
            _moveInputAxis = pContext.ReadValue<Vector2>();
        }

        public void ActionInput(InputAction.CallbackContext pContext)
        {
            if (pContext.performed)
            {
                Action();
            }
        }

        #endregion
        
        #region ACTIONS

        private void MoveAction()
        {
            Vector2 lAxisSpeed = _moveInputAxis * (moveSpeed * Time.fixedDeltaTime);
            Vector3 lAxis = new Vector3(lAxisSpeed.x, _rb.linearVelocity.y, lAxisSpeed.y);

            Vector3 lNewVel = lAxis.magnitude != 0 ? Vector3.Lerp(_rb.linearVelocity,  lAxis, acceleration) : 
                                                    Vector3.Lerp(_rb.linearVelocity,  Vector3.zero, deceleration);
            _rb.linearVelocity = lNewVel;
        }

        private void Action()
        {
            foreach (var lEffect in explosionEffects)
            {
                Instantiate(lEffect, transform.position, transform.rotation);
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundInstantiate(5);
            }
            
            Collider[] lEnnemies = Physics.OverlapSphere(transform.position, 10);
                
            foreach (var lEnnemyEntity in lEnnemies)
            {
                if (lEnnemyEntity.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    lEnnemyEntity.gameObject.GetComponent<EnemyBasic>().onDeath.Invoke();
                }
            }
            
            //StartCoroutine(ExplosionPropagation(transform.position));
        }

        IEnumerator ExplosionPropagation(Vector3 pOrigin)
        {
            foreach (var lEffect in explosionEffects)
            {
                Instantiate(lEffect, transform.position, transform.rotation);
            }
            float radius = 10f;
            Vector3 lPreviousPosition = pOrigin;
            
            while (radius < maxExplosionRadius)
            {
                //radius += propagationSpeed * Time.deltaTime;
                Collider[] lEnnemies = Physics.OverlapSphere(lPreviousPosition, radius);
                
                foreach (var lEnnemyEntity in lEnnemies)
                {
                    if (lEnnemyEntity.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                    {
                        Destroy(lEnnemyEntity);
                    }
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
        }
        
        private void AutoShoot()
        {
            Vector3 lCenter = transform.position;
            Collider[] lListOfEnnemy = Physics.OverlapSphere(lCenter, autoShootRadiusDetection, LayerMask.GetMask("Enemies"));
            
            if (_autoShootTimer <= bulletSpawnTime)
            {
                _autoShootTimer += Time.deltaTime;
            }
            else
            {
                foreach (var enemyColliders in lListOfEnnemy)
                {
                    float lDistance = (enemyColliders.gameObject.transform.position - transform.position).magnitude;
                    if (!(lDistance <= minDistance)) continue;
                    
                    transform.LookAt(enemyColliders.gameObject.transform.position);
                    
                    Quaternion lRotation = Quaternion.LookRotation(enemyColliders.gameObject.transform.forward * -1);
                    Instantiate(bullet, transform.position, lRotation);
                    break;
                }
                _autoShootTimer = 0;
            }
        }
        #endregion

        #region SCORING

        public void UpdateScore(float pValue)
        {
            Score += pValue;
        }

        public float GetScore()
        {
            return Score;
        }

        #endregion
        
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, autoShootRadiusDetection);
        }
#endif
    }
    
}
