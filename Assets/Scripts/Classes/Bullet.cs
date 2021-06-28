// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private int _damageAmount = 1;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            Destroy(gameObject, 3.0f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                var damage = collision.collider.GetComponentInParent<IDamageable>();
                if (damage != null)
                 damage.Damage(_damageAmount);
            }
            Destroy(gameObject);
        }
    }
}