// Aaron Grincewicz Veganimus@icloud.com 6/5/2021

using System;
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bomb : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField,Tooltip("Max Level of Doors which can be opened by this bomb.")]
        private int _maxDoorLevel = 1;
        [SerializeField] private int _maxColliders = 50;
        [SerializeField, Tooltip("Time after instantiation when bomb detonates.")]
        private float _bombTimer;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _upForce;
        [SerializeField] private int _damageAmount = 2;       
        [SerializeField] private LayerMask _targetLayers = 0;
        private bool _detonationTriggered = false;
        private IBombable _iBombable;
        private IDamageable _idamageable;
        private Collider[] _hitColliders;
        private WaitForSeconds _explosionDelay;
        public int MaxDoorLevel { get => _maxDoorLevel; set { } }

        //private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _explosionRadius)
        private void Awake() => _explosionDelay = new WaitForSeconds(_bombTimer);

        private void Start() => StartCoroutine(ExplosionRoutine());

        private void FixedUpdate()
        {
            if (_detonationTriggered)
                Detonate();
        }
        private void Detonate()
        {
            _hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _targetLayers);
           
            for(int i = 0; i< _hitColliders.Length; i++)
            {
                Rigidbody _rb = _hitColliders[i].GetComponent<Rigidbody>();
                _iBombable = _hitColliders[i].GetComponentInParent<IBombable>();
                _idamageable = _hitColliders[i].GetComponentInParent<IDamageable>();
                if (_rb != null && _iBombable != null)
                {
                    _rb.useGravity = true;
                    _rb.isKinematic = false;
                    _rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upForce, ForceMode.Impulse);
                    if (_idamageable != null && !_idamageable.IsPlayer)
                    {
                        _idamageable.Damage(_damageAmount);
                    }
                    else if (_idamageable == null)
                        Destroy(_hitColliders[i].gameObject, 1.0f);
                }
            }
        }
        private IEnumerator ExplosionRoutine()
        {
            yield return _explosionDelay;
            _detonationTriggered = true;
            Destroy(gameObject, 0.25f);
        }
    }
}