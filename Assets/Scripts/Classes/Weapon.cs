// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField, Tooltip("Indicates if alternate weapon active.")]
        protected bool _isSecondaryFireOn = false;
        [SerializeField, Tooltip("Secondary Weapon Ammo Count")]
        protected int _secondaryAmmo = 0;
        [SerializeField, Tooltip("Primary Weapon Fire Rate")]
        protected float _primaryWeaponFireRate = 0.5f;
        [SerializeField, Tooltip("Secondary Weapon Fire Rate")]
        protected float _secondaryWeaponFireRate = 0.5f;
        [SerializeField, Tooltip("Projectile Prefab for Primary Weapon.")]
        protected GameObject _primaryWeaponPrefab;
        [SerializeField, Tooltip("Projectile Prefab for Secondary Weapon.")]
        protected GameObject _secondaryWeaponPrefab;
        [SerializeField,Tooltip("Position at which projectiles are instantiated.")]
        protected Transform _fireOffset;
        protected bool _secondaryFireTriggered;
        protected float _canFire = -1.0f;
        protected PoolManager _poolManager;
        protected Transform _pmTransform;
        private Character _player;
        private UIManager _uIManager;
        protected WaitForSeconds _secondaryWeaponFireCooldownTime, _primaryWeaponFireCooldownTime;
        public int SecondaryAmmo { get { return _secondaryAmmo; } set { _secondaryAmmo = value; } }

        private void OnEnable() => _player = GetComponentInParent<Character>();

        protected virtual IEnumerator Start()
        {
            yield return new WaitForSeconds(1.0f);
            _primaryWeaponFireCooldownTime = new WaitForSeconds(_primaryWeaponFireRate);
            _secondaryWeaponFireCooldownTime = new WaitForSeconds(_secondaryWeaponFireRate);
            _poolManager = PoolManager.Instance;
            _pmTransform = _poolManager.transform;
            _uIManager = UIManager.Instance;
           _uIManager.MissilesTextUpdate(_secondaryAmmo);
        }

        protected virtual void Update()
        {
            if (_player.Upgrades.missiles)
            {
                _secondaryFireTriggered = _inputManager.controls.Standard.SecondaryFire.triggered;
                if (_secondaryFireTriggered && !_isSecondaryFireOn)
                    SecondaryWeaponActiveUIUpdate(true);
                else if (_secondaryFireTriggered && _isSecondaryFireOn || _secondaryAmmo <= 0)
                    SecondaryWeaponActiveUIUpdate(false);
            }
            
            switch(_isSecondaryFireOn)
            {
                case true:
                    if (Time.time > _canFire && _secondaryAmmo > 0 && _player.Upgrades.missiles)
                        FireSecondaryWeapon();
                        break;
                case false:
                    if (Time.time > _canFire)
                        FirePrimaryWeapon();
                    break;
            }
        }
       
        protected virtual void FirePrimaryWeapon()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _primaryWeaponFireRate;
                Instantiate(_primaryWeaponPrefab, _fireOffset.position, _fireOffset.rotation, _pmTransform);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        protected virtual void FireSecondaryWeapon()
        {
            var shootTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (shootTriggered)
            {
                _canFire = Time.time + _secondaryWeaponFireRate;
                _secondaryAmmo--;
                _uIManager.MissilesTextUpdate(_secondaryAmmo);
                Instantiate(_secondaryWeaponPrefab, _fireOffset.position, _fireOffset.rotation, _pmTransform);
            }
            StartCoroutine(ShootCoolDownRoutine());
        }
        protected IEnumerator ShootCoolDownRoutine()
        {
            if (!_isSecondaryFireOn)
                yield return _primaryWeaponFireCooldownTime;
            else
                yield return _secondaryWeaponFireCooldownTime;
        }
        private void SecondaryWeaponActiveUIUpdate(bool isOn)
        {
            _isSecondaryFireOn = isOn;
            _uIManager.SecondaryFireActive(isOn);
        }
    }
}