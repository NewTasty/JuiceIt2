using System.Collections;
using JuiceIt2Content.Programming.Enemy;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private float effectDuration = 2;
        [SerializeField] private float explosionDelay = 2;
        [SerializeField] private GameObject[] explosionEffects;
 
        private Rigidbody _rb;
        private Vector2 _moveInputAxis;

        private SoundManager _soundManager;
        
        private float _autoShootTimer;
        
        private static float Score {get; set;}

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _soundManager = FindFirstObjectByType<SoundManager>();
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

        public void UltimateInput(InputAction.CallbackContext pContext)
        {
            if (pContext.performed)
            {
                Ultimate();
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

        private void Ultimate()
        {
            StartCoroutine(ExplosionDelayCoroutine());
        }

        IEnumerator ExplosionDelayCoroutine()
        {
            foreach (var lEffect in explosionEffects)
            {
                Instantiate(lEffect, transform.position, transform.rotation);
                _soundManager.SoundInstantiate(5, lEffect.transform);
            }
            
            yield return new WaitForSeconds(explosionDelay);
            
            Collider[] lEnnemies = Physics.OverlapSphere(transform.position, maxExplosionRadius);
                
            foreach (var lEnnemyEntity in lEnnemies)
            {
                if (lEnnemyEntity.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    lEnnemyEntity.gameObject.GetComponent<EnemyBasic>().onDeath.Invoke();
                }
            }
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
