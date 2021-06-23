using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Veganimus.Platformer
{
   
    public class InputManager :MonoBehaviour
    {
        public UnityAction<float, float> moveAction;
        public UnityAction<float> upAimAction;
        public UnityAction<float> downAimAction;
        public UnityAction shootAction;
        public UnityAction ballModeAction;
        public UnityAction jumpAction;

        public Controls controls;

        private void OnEnable()
        {
            controls = new Controls();
            controls.Standard.Enable();
            controls.Standard.Movement.performed += OnMoveInput;
            controls.Standard.UpAIm.performed += OnUpAimInput;
            controls.Standard.UpAIm.canceled += OnUpAimInput;
            controls.Standard.DownAim.performed += OnDownAimInput;
            controls.Standard.DownAim.canceled += OnDownAimInput;
          
        }

        private void OnDownAimInput(InputAction.CallbackContext obj)
        {
            float downAimInput = obj.ReadValue<float>();
            if (downAimAction != null)
                downAimAction.Invoke(downAimInput);
        }

        private void OnUpAimInput(InputAction.CallbackContext obj)
        {
            float upAimInput = obj.ReadValue<float>();
            if (upAimAction != null)
                upAimAction.Invoke(upAimInput);
        }

        private void OnDisable()
        {
            controls.Disable();
            controls.Standard.Disable();
            controls.Standard.Movement.performed -= OnMoveInput;
        }
        
        private void OnMoveInput(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            if (moveAction != null)
                moveAction.Invoke(moveInput.x, moveInput.y);
        }
       
    }
}
