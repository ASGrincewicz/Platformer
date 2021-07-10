// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _fireOffset;
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField] protected float _fireRate = 0.5f;
        protected PoolManager _poolManager;
        protected float _canFire = -1.0f;
        protected WaitForSeconds _shootCoolDown;

        protected void Start()
        {
            _shootCoolDown = new WaitForSeconds(_fireRate);
            _poolManager = PoolManager.Instance;
        }

        protected virtual void Update()
        {
            if (Time.time > _canFire)
             Shoot();
        }
       
        protected virtual void Shoot()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_bulletPrefab, _fireOffset.transform.position, _fireOffset.rotation, _poolManager.transform);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        protected IEnumerator ShootCoolDownRoutine()
        {
            yield return _shootCoolDown;
        }
    }
}