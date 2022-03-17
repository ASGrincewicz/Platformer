// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField] private sbyte _damageAmount = 1;
        [SerializeField] private byte _maxDoorLevel = 0;
        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _speed = 10.0f;
        public byte MaxDoorLevel { get { return _maxDoorLevel; } set { } }
        private float _deltaTime;
        private IDamageable _iDamageable;
        private Door _door;
        private Transform _transform;

        private void Start() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _transform.Translate(Vector3.forward * _speed * _deltaTime);
            Destroy(gameObject, _lifeTime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                _iDamageable = collision.collider.GetComponentInParent<IDamageable>();
                _door = collision.collider.GetComponentInParent<Door>();
                if (_iDamageable != null)
                {
                    _iDamageable.Damage(_damageAmount);
                    Destroy(gameObject);
                }
                else if (_iDamageable == null && _door != null && _door.DoorLevel > _maxDoorLevel)
                    Destroy(gameObject);
                else
                    Destroy(gameObject);
            }
        }
    }
}