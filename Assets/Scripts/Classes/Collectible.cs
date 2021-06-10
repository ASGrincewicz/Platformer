// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Collectible : MonoBehaviour, IAbsorbable
    {
        [SerializeField] private bool _canAbosrb = false;
        public bool CanAbsorb { get { return _canAbosrb; } }
        [SerializeField] private LayerMask _collectorLayerMask;

        private void OnTriggerEnter(Collider other)
        {
            if (_canAbosrb)
            {
                var absorber = other.GetComponent<IAbsorbable>();
                if (absorber != null)
                {
                    transform.localScale = new Vector3(2, 2, 2);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
