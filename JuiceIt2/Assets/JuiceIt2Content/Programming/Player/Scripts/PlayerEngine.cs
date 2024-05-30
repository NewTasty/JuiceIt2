using System.Collections;
using JuiceIt2Content.Programming.Enemy;
using JuiceIt2Content.Programming.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class PlayerEngine : MonoBehaviour
    {
        [SerializeField, Header("Movement")] private int _moveSpeed = 500;
        [SerializeField] private float _acceleration = 0.3f;
        [SerializeField] private float _deceleration = 0.3f;
        [SerializeField, Space, Header("Stats")] private int _maxLife;
        [SerializeField, Space, Header("AutoShoot")] private float _bulletSpawnTime = 1;
        [SerializeField] private float _minDistance = 10;
        [SerializeField] private float _autoShootRadiusDetection = 15;
        [SerializeField] private GameObject _bullet;
        [SerializeField, Space, Header("Ult")]private float _maxExplosionRadius = 15;
        [SerializeField] private float _explosionDelay = 2;
        [SerializeField] private float _explosionCooldown = 2;
        [SerializeField] private GameObject[] _explosionEffects;

        private int _lifePoint;
        
        private Rigidbody _rb;
        private Vector2 _moveInputAxis;
        
        private float _autoShootTimer;
        private float _ultTimer;
        private bool _ultAction;
        
        private static float Score {get; set;}

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _lifePoint = GetMaxLife();
            Score = 0;
        }

        private void FixedUpdate()
        {
            MoveAction();
        }

        private void Update()
        {
            AutoShoot();
            if (!_ultAction) return;
            
            if (_ultTimer < _explosionCooldown)
            {
                _ultTimer += Time.deltaTime;
            }
            else
            {
                _ultTimer = 0;
                _ultAction = false;
            }

        }

        #region INPUTS

        public void MoveInput(InputAction.CallbackContext pContext)
        {
            _moveInputAxis = pContext.ReadValue<Vector2>();
        }

        public void UltimateInput(InputAction.CallbackContext pContext)
        {
            if (pContext.performed && !_ultAction)
            {
                _ultAction = true;
                Ultimate();
            }
        }
        
        #endregion
        
        #region ACTIONS

        private void MoveAction()
        {
            Vector2 lAxisSpeed = _moveInputAxis * (_moveSpeed * Time.fixedDeltaTime);
            Vector3 lAxis = new Vector3(lAxisSpeed.x, _rb.linearVelocity.y, lAxisSpeed.y);

            Vector3 lNewVel = lAxis.magnitude != 0 ? Vector3.Lerp(_rb.linearVelocity,  lAxis, _acceleration) : 
                                                    Vector3.Lerp(_rb.linearVelocity,  Vector3.zero, _deceleration);
            _rb.linearVelocity = lNewVel;
        }

        private void Ultimate()
        {
            StartCoroutine(ExplosionDelayCoroutine());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator ExplosionDelayCoroutine()
        {
            StartCoroutine(StartShake());
            
            foreach (var lEffect in _explosionEffects)
            {
                var effect = Instantiate(lEffect, transform.position, transform.rotation);
                GameMode.SoundManager.SoundInstantiate(5, effect.transform);
            }
            
            yield return new WaitForSeconds(_explosionDelay);
            
            Collider[] lEnnemies = Physics.OverlapSphere(transform.position, _maxExplosionRadius);
                
            foreach (var lEnnemyEntity in lEnnemies)
            {
                if (lEnnemyEntity.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    lEnnemyEntity.gameObject.GetComponent<EnemyBasic>().onDeath.Invoke();
                }
            }
        }

        private IEnumerator StartShake()
        {
            yield return new WaitForSeconds(1f);
            CameraEngine.SetShake(true);
            yield return new WaitForSeconds(0.7f);
            CameraEngine.SetShake(false);
            yield return this;
        }
        
        private void AutoShoot()
        {
            Vector3 lCenter = transform.position;
            Collider[] lListOfEnnemy = Physics.OverlapSphere(lCenter, _autoShootRadiusDetection, LayerMask.GetMask("Enemies"));
            
            if (_autoShootTimer <= _bulletSpawnTime)
            {
                _autoShootTimer += Time.deltaTime;
            }
            else
            {
                foreach (var enemyColliders in lListOfEnnemy)
                {
                    float lDistance = (enemyColliders.gameObject.transform.position - transform.position).magnitude;
                    if (!(lDistance <= _minDistance)) continue;
                    
                    transform.LookAt(enemyColliders.gameObject.transform.position);
                    
                    Quaternion lRotation = Quaternion.LookRotation(enemyColliders.gameObject.transform.forward * -1);
                    Instantiate(_bullet, transform.position, lRotation);
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

        #region LIFE

        public int GetLife()
        {
            return _lifePoint;
        }

        public int GetMaxLife()
        {
            return _maxLife;
        }
        
        public void TakeDamage(int pDamage)
        {
            int newLife = Mathf.Clamp(_lifePoint, 0, _maxLife);
            _lifePoint = newLife - pDamage;
        }
        #endregion
        
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _autoShootRadiusDetection);
        }
#endif
    }
    
}
