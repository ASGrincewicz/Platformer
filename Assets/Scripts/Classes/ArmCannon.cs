// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class ArmCannon : MonoBehaviour
    {
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private float _fireRate = 0.5f;
        private float _canFire = -1.0f;
        private WaitForSeconds _shootCoolDown;

        private void Start()
        {
            _shootCoolDown = new WaitForSeconds(_fireRate);
        }
        private void Update()
        {
            if (Time.time > _canFire)
            {
                Shoot();
            }
        }
        private void Shoot()
        {
           
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _fireRate;
                var newBullet = Instantiate(_bulletPrefab, _fireOffset.transform.position, _fireOffset.rotation);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        private IEnumerator ShootCoolDownRoutine()
        {
            yield return _shootCoolDown;
        }
    }
}