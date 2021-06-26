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

        private void Start()
        {
            _explosionDelay = new WaitForSeconds(_bombTimer);
            StartCoroutine(ExplosionRoutine());
        }
        private void Detonate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach(Collider hit in colliders)
            {
                Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
                var bombable = hit.GetComponentInParent<IBombable>();
                var damageable = hit.GetComponentInParent<IDamageable>();
                if (rigidbody != null && bombable != null)
                {
                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upForce, ForceMode.Impulse);
                    if (damageable != null && damageable.IsPlayer == false)
                    {
                        Debug.Log($"Found Damageable in {hit.name}");
                        damageable.Damage(_damageAmount);
                    }
                    else if(damageable == null && damageable.IsPlayer == false)
                        Destroy(hit.gameObject, 1.0f);
                }
            }
        }
        private IEnumerator ExplosionRoutine()
        {
            yield return _explosionDelay;
            Detonate();
            Destroy(this.gameObject, 1f);
        }
    }
}