// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class EnemyWeapon : Weapon
    {
        public bool IsShooting { get; set; }

        protected override IEnumerator Start()
        {
            yield return new WaitForSeconds(1.0f);
            _primaryWeaponFireCooldownTime = new WaitForSeconds(_primaryWeaponFireRate);
            _secondaryWeaponFireCooldownTime = new WaitForSeconds(_secondaryWeaponFireRate);
            _poolManager = PoolManager.Instance;
            _pmTransform = _poolManager.transform;
        }
        protected override void Update()
        {
            if (Time.time > _canFire)
                FirePrimaryWeapon();
        }

        protected override void FirePrimaryWeapon()
        {
            if (IsShooting)
            {
                _canFire = Time.time + _primaryWeaponFireRate;
                Instantiate(_primaryWeaponPrefab, _fireOffset.position, _fireOffset.rotation, _pmTransform);
                
                StartCoroutine(ShootCoolDownRoutine());
            }
        }
        protected override void FireSecondaryWeapon(){}
    }
}