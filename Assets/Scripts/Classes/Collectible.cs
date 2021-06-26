// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Collectible : MonoBehaviour, IAbsorbable
    {
        private float _xRadius;
        private float _yRadius;
        private float _time = 0;
        private bool _collected;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _collectorLayerMask;
        [SerializeField] private bool _canAbosrb = false;
        public bool CanAbsorb { get { return _canAbosrb; } }

        private void Start()
        {
            _xRadius = Random.Range(-0.05f, 0.05f);
            _yRadius = Random.Range(-0.05f, 0.05f);
        }

        private void Update()
        {
            if (!_collected)
            {
                _time += Time.deltaTime;
                float x = _xRadius * Mathf.Cos(_time * _speed);
                float y = _yRadius * Mathf.Sin(_time * _speed);
                transform.position += new Vector3(x, y, 0);
            }
           
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterController>())
            {
                _collected = true;
                UIManager.Instance.UpdateCollectibleText(1);
                Destroy(this.gameObject);
            }
        }
    }
}
