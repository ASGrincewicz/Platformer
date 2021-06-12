using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Veganimus.Platformer
{
   
    public class InputManager :MonoBehaviour
    {
        public UnityAction<float, float> moveAction;
        public UnityAction shootAction;
        public UnityAction ballModeAction;
        public UnityAction jumpAction;

        public Controls controls;

        private void OnEnable()
        {
            controls = new Controls();
            controls.Standard.Enable();
            controls.Standard.Movement.performed += OnMoveInput;
            //controls.Standard.Jump.performed += OnJumpInput;
            //controls.Standard.Jump.canceled += OnJumpInput;
            //controls.Standard.BallMode.performed += OnBallModeInput;
        }
        private void OnDisable()
        {
            controls.Disable();
            controls.Standard.Disable();
            controls.Standard.Movement.performed -= OnMoveInput;
            //controls.Standard.Jump.performed -= OnJumpInput;
            //controls.Standard.Jump.canceled -= OnJumpInput;
            //controls.Standard.BallMode.performed += OnBallModeInput;

        }
        
        private void OnMoveInput(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            if (moveAction != null)
                moveAction.Invoke(moveInput.x, moveInput.y);
        }
        //private void OnJumpInput(InputAction.CallbackContext obj)
        //{
        //    //bool isJumping = obj.ReadValueAsButton();
        //    if (jumpAction != null)
        //        jumpAction.Invoke();
        //}
        //private void OnBallModeInput(InputAction.CallbackContext obj)
        //{
        //    if (ballModeAction != null)
        //        ballModeAction.Invoke();
        //}
    }
}
