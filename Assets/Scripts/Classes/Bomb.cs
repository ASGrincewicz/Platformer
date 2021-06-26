// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bomb : MonoBehaviour
    {
        private WaitForSeconds _explosionDelay;
        [SerializeField] private float _bombTimer;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _upForce;
        [SerializeField] private int _damageAmount = 2;
        [SerializeField] private LayerMask _targetLayers;
        public Collider[] hitColliders;

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
            int maxColliders = 50;
            hitColliders = new Collider[maxColliders];
            int numberColliders =  Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, hitColliders,_targetLayers);
            for(int i = 0; i< numberColliders; i++)
            {
                Rigidbody rigidbody = hitColliders[i].GetComponent<Rigidbody>();
                var bombable = hitColliders[i].GetComponentInParent<IBombable>();
                var damageable = hitColliders[i].GetComponentInParent<IDamageable>();
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
                        Destroy(hitColliders[i].gameObject, 1.0f);
                    //else if (damageable != null && damageable.IsPlayer == true)
                    //    return;
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