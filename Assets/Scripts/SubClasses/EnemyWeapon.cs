// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class EnemyWeapon : Weapon
    {
        public bool isShooting;
        

        protected override void Update()
        {
            if (Time.time > _canFire)
            {
                EnemyShoot();
            }
        }

        public void EnemyShoot()
        {
            if (isShooting)
            {
                _canFire = Time.time + _fireRate;
                var enemyBullet = Instantiate(_bulletPrefab,_fireOffset.transform.position, _fireOffset.rotation, _poolManager.transform);
                StartCoroutine(ShootCoolDownRoutine());
            }
        }
    }
}