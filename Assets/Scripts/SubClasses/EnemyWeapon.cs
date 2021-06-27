// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class EnemyWeapon : Weapon
    {
        protected override void Shoot()
        {
            _canFire = Time.time + _fireRate;
            var newBullet = Instantiate(_bulletPrefab, _fireOffset.transform.position, _fireOffset.rotation, _poolManager.transform);
            StartCoroutine(ShootCoolDownRoutine());
        }

    }
}