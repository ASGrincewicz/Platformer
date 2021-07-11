// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private sbyte _damageAmount = 1;
        [SerializeField] private byte _maxDoorLevel = 0;
        public byte MaxDoorLevel { get { return _maxDoorLevel; } set { value = _maxDoorLevel; } }
        private Transform _transform;
        private float _globalDeltaTime;

        private void Start() => _transform = transform;

        private void FixedUpdate()
        {
            _globalDeltaTime = Time.deltaTime;
            _transform.Translate(Vector3.forward * _speed * _globalDeltaTime);
            Destroy(gameObject, _lifeTime);
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
                    Destroy(gameObject);
                else
                    Destroy(gameObject);
            }
        }
    }
}