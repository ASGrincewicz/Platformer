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
                var bombable = hit.GetComponent<IBombable>();
                if (rigidbody != null && bombable != null)
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 3.0f, ForceMode.Impulse);
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