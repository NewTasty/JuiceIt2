using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JuiceIt2Content.Programming.Player
{
    public class PlayerEngine : MonoBehaviour
    {
        [SerializeField] private int moveSpeed = 500;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float deceleration = 0.3f;
        [SerializeField] private float baseFireSpeed = 1;
        [SerializeField] private float baseFirePower = 1;

        private Rigidbody _rb;
        private Vector2 _moveInputAxis;

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
            
        }

        #endregion
    }
    
}
