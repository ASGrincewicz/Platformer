// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private int _maxColliders = 50;
        [SerializeField] private float _bombTimer;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _upForce;
        [SerializeField] private int _damageAmount = 2;
        [SerializeField] private LayerMask _targetLayers;
        private WaitForSeconds _explosionDelay;
        private Collider[] _hitColliders;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _explosionRadius)
;        }

        private void Start()
        {
            _explosionDelay = new WaitForSeconds(_bombTimer);
            StartCoroutine(ExplosionRoutine());
        }
        private void Detonate()
        {
            _hitColliders = new Collider[_maxColliders];
            int numberColliders =  Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _hitColliders,_targetLayers);
            for(int i = 0; i< numberColliders; i++)
            {
                Rigidbody rigidbody = _hitColliders[i].GetComponent<Rigidbody>();
                var bombable = _hitColliders[i].GetComponentInParent<IBombable>();
                var damageable = _hitColliders[i].GetComponentInParent<IDamageable>();
                if (rigidbody != null && bombable != null)
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upForce, ForceMode.Impulse);
                    if (damageable != null && damageable.IsPlayer == false)
                    {
                        damageable.Damage(_damageAmount);
                    }
                    else if (damageable == null)
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