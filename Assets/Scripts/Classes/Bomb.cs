// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bomb : MonoBehaviour, ICanOpenDoor
    {
        [SerializeField] private byte _maxDoorLevel = 1;
        [SerializeField] private byte _maxColliders = 50;
        [SerializeField] private float _bombTimer;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _upForce;
        [SerializeField] private sbyte _damageAmount = 2;
        [SerializeField] private LayerMask _targetLayers = 0;
        private Collider[] _hitColliders;
        private IBombable _iBombable;
        private IDamageable _idamageable;
        private WaitForSeconds _explosionDelay;
        public byte MaxDoorLevel { get { return _maxDoorLevel; } set { value = _maxDoorLevel; } }

        //private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _explosionRadius)

        private void Start()
        {
            _explosionDelay = new WaitForSeconds(_bombTimer);
            StartCoroutine(ExplosionRoutine());
        }
        private void Detonate()
        {
            _hitColliders = new Collider[_maxColliders];
            byte numberColliders =  (byte)Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _hitColliders,_targetLayers);
            for(byte i = 0; i< numberColliders; i++)
            {
                Rigidbody rigidbody = _hitColliders[i].GetComponent<Rigidbody>();
                _iBombable = _hitColliders[i].GetComponentInParent<IBombable>();
                _idamageable = _hitColliders[i].GetComponentInParent<IDamageable>();
                if (rigidbody != null && _iBombable != null)
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upForce, ForceMode.Impulse);
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
            Detonate();
            Destroy(this.gameObject, 0.25f);
        }
    }
}