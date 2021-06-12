using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Veganimus.Platformer
{
    [CreateAssetMenu(menuName ="Input Manager")]
    public class InputManager : ScriptableObject
    {
        public UnityAction<float, float> moveAction;
        public UnityAction shootAction;
        public UnityAction ballModeAction;
        public UnityAction jumpAction;

        private Controls _controls;

        private void OnEnable()
        {
            _controls = new Controls();
            _controls.Standard.Enable();
            _controls.Standard.Movement.performed += OnMoveInput;
            _controls.Standard.Jump.performed += OnJumpInput;
            _controls.Standard.BallMode.performed += OnBallModeInput;
        }
        private void OnDisable()
        {
            _controls.Disable();
            _controls.Standard.Disable();
            _controls.Standard.Movement.performed -= OnMoveInput;
            _controls.Standard.Jump.performed -= OnJumpInput;
            _controls.Standard.BallMode.performed += OnBallModeInput;

        }
        private void OnMoveInput(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            if (moveAction != null)
                moveAction.Invoke(moveInput.x, moveInput.y);
        }
        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            if (jumpAction != null)
                jumpAction.Invoke();
        }
        private void OnBallModeInput(InputAction.CallbackContext obj)
        {
            if (ballModeAction != null)
                ballModeAction.Invoke();
        }
    }
}
