// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private sbyte _damageAmount = 1;
        [SerializeField] private byte _maxDoorLevel = 0;
        public byte MaxDoorLevel { get { return _maxDoorLevel; } set { value = _maxDoorLevel; } }

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
                var door = collision.collider.GetComponentInParent<Door>();
                if (damage != null)
                {
                    damage.Damage(_damageAmount);
                    Destroy(gameObject);
                }
                else if (damage == null && door != null && door.DoorLevel > _maxDoorLevel)
                    Destroy(gameObject, 2.0f);
                else
                    Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
    }
}