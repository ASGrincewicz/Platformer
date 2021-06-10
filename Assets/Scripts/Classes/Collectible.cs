// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Collectible : MonoBehaviour, IAbsorbable
    {
        [SerializeField] private bool _canAbosrb = false;
        public bool CanAbsorb { get { return _canAbosrb; } }
        [SerializeField] private LayerMask _collectorLayerMask;
        private float _xRadius;
        private float _yRadius;
        [SerializeField] private float _speed;
        private float _time = 0;

        private void Start()
        {
            _xRadius = Random.Range(-0.05f, 0.05f);
            _yRadius = Random.Range(-0.05f, 0.05f);
        }

        private void Update()
        {
           _time += Time.deltaTime;
            float x = _xRadius * Mathf.Cos(_time * _speed);
            float y = _yRadius * Mathf.Sin(_time * _speed);
            transform.position += new Vector3(x,y , 0);
           
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (_canAbosrb)
        //    {
        //        var absorber = other.GetComponent<IAbsorbable>();
        //        if (absorber != null)
        //        {
        //            transform.localScale = new Vector3(2, 2, 2);
        //            Destroy(other.gameObject);
        //        }
        //    }
        //}
    }
}
