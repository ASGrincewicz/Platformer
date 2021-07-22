// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField] protected bool _isSecondaryFireOn = false;
        [SerializeField] protected int _secondaryAmmo = 5;
        [SerializeField] protected float _fireRate = 0.5f, _secondaryFireRate = 0.5f;
        [SerializeField] protected GameObject _bulletPrefab, _missilePrefab;
        [SerializeField] protected Transform _fireOffset;
        protected bool _secondaryFireTriggered;
        protected float _canFire = -1.0f;
        protected PoolManager _poolManager;
        protected Transform _pmTransform;
        protected WaitForSeconds _secondaryCoolDown, _shootCoolDown;
        public int SecondaryAmmo { get { return _secondaryAmmo; } }

        protected virtual IEnumerator Start()
        {
            yield return new WaitForSeconds(1.0f);
            _shootCoolDown = new WaitForSeconds(_fireRate);
            _secondaryCoolDown = new WaitForSeconds(_secondaryFireRate);
            _poolManager = PoolManager.Instance;
            _pmTransform = _poolManager.transform;
           UIManager.Instance.MissilesTextUpdate(_secondaryAmmo);
        }

        protected virtual void Update()
        {
            _secondaryFireTriggered = _inputManager.controls.Standard.SecondaryFire.triggered;
            if (_secondaryFireTriggered && !_isSecondaryFireOn)
                SecondaryUIUpdate(true);
            else if (_secondaryFireTriggered && _isSecondaryFireOn || _secondaryAmmo <= 0)
                SecondaryUIUpdate(false);
            
            switch(_isSecondaryFireOn)
            {
                case true:
                    if (Time.time > _canFire && _secondaryAmmo > 0)
                        SecondaryShoot();
                        break;
                case false:
                    if (Time.time > _canFire)
                        Shoot();
                    break;
            }
        }
       
        protected virtual void Shoot()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_bulletPrefab, _fireOffset.position, _fireOffset.rotation, _pmTransform);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        protected virtual void SecondaryShoot()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _secondaryFireRate;
                _secondaryAmmo--;
                UIManager.Instance.MissilesTextUpdate(_secondaryAmmo);
                Instantiate(_missilePrefab, _fireOffset.position, _fireOffset.rotation, _pmTransform);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        protected IEnumerator ShootCoolDownRoutine()
        {
            if (!_isSecondaryFireOn)
                yield return _shootCoolDown;
            else
                yield return _secondaryCoolDown;
        }
        private void SecondaryUIUpdate(bool isOn)
        {
            _isSecondaryFireOn = isOn;
            UIManager.Instance.SecondaryFireActive(isOn);
        }
    }
}