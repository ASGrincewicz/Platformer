// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class EnemyWeapon : Weapon
    {
       
        public bool IsShooting { get; set; }

        protected override void Update()
        {
            if (Time.time > _canFire)
                Shoot();
        }

        protected override void Shoot()
        {
            if (IsShooting)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_bulletPrefab, _fireOffset.transform.position, _fireOffset.rotation, _poolManager.transform);
                
                StartCoroutine(ShootCoolDownRoutine());
            }
        }
    }
}