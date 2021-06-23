// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class ArmCannon : MonoBehaviour
    {
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private InputManager _inputManager;
        private float _fireRate = 0.5f;

        private void Update()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                var newBullet = Instantiate(_bulletPrefab, _fireOffset.transform.position, _fireOffset.rotation);
            }
        }
    }
}