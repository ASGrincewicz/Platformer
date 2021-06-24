using UnityEngine;

namespace Veganimus.Platformer
{
    public class BallSpace : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Character>())
            {
                _inputManager.controls.Standard.BallMode.Disable();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<Character>())
            {
                _inputManager.controls.Standard.BallMode.Enable();
            }
        }
    }
}