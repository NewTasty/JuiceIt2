using UnityEngine;
using UnityEngine.InputSystem;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class PlayerEngine : MonoBehaviour
    {
        [SerializeField] private int moveSpeed = 500;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float deceleration = 0.3f;
        [SerializeField] private float baseFireSpeed = 1;
        [SerializeField] private float baseFirePower = 1;
        [SerializeField] private float autoShootRadiusDetection = 15;
        [SerializeField] private GameObject bullet;

        private Rigidbody _rb;
        private Vector2 _moveInputAxis;
        private static float Score { get; set; }

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
            print("FireSpecial");
        }

        private void AutoShoot()
        {
            Vector3 lCenter = transform.position;
            Collider[] lListOfEnnemy = Physics.OverlapSphere(lCenter, autoShootRadiusDetection, LayerMask.GetMask("Enemies"));

            float lTimer = 0;

            if (lTimer >= baseFireSpeed)
            {
                lTimer += Time.deltaTime;
                print(lTimer);
            }
            else lTimer = 0;
            
            foreach (var variable in lListOfEnnemy)
            {
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, autoShootRadiusDetection);
        }

        #endregion
        
        public void UpdateScore(float pValue)
        {
            Score += pValue;
        }

        public float GetScore()
        {
            return Score;
        }
    }
    
}
