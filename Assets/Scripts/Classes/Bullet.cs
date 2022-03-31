// Aaron Grincewicz Veganimus@icloud.com 6/5/2021

using System;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField, Tooltip("Damage this projectile inflicts.")]
        private int _damageAmount = 1;
        [SerializeField, Tooltip("Max Level of Door this projectile can open.")]
        private int _maxDoorLevel = 0;
        [SerializeField,Tooltip("Time before object is destroyed if no collision detected.")]
        private float _lifeTime = 5.0f;
        [SerializeField, Tooltip("Rate at which projectile travels.")]
        private float _speed = 10.0f;
        public int MaxDoorLevel { get => _maxDoorLevel; set { } }
        private float _deltaTime;
        private IDamageable _iDamageable;
        private Door _door;
        private Transform _transform;

        private void Awake() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _transform.Translate(Vector3.forward * _speed * _deltaTime);
            Destroy(gameObject, _lifeTime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null) return;
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