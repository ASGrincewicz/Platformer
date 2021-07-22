// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Collectible : MonoBehaviour, IAbsorbable
    {
        [SerializeField] private bool _canAbosrb = false;
        [SerializeField] private sbyte _powerUpAmount;
        [SerializeField] private float _speed;
        [SerializeField] private CollectibleType _collectibleType;
        [SerializeField] private LayerMask _collectorLayerMask;
        private bool _collected;
        private float _deltaTime;
        private float _time = 0;
        private float _x, _y, _z = 0;
        private float _xRadius, _yRadius;
        private Transform _transform;
        public bool CanAbsorb { get { return _canAbosrb; } }

        private void Start()
        {
            _transform = transform;
            _xRadius = Random.Range(-0.05f, 0.05f);
            _yRadius = Random.Range(-0.05f, 0.05f);
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            if (Time.timeScale > 0)
            {
                _time += _deltaTime;
                _x = _xRadius * Mathf.Cos(_time * _speed);
                _y = _yRadius * Mathf.Sin(_time * _speed);
                _transform.position += new Vector3(_x, _y, _z);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                if(_canAbosrb)
                {
                    switch(_collectibleType)
                    {
                        case CollectibleType.Health:
                            other.GetComponent<PlayerHealth>().Heal((sbyte)(_powerUpAmount / 2));
                            break;
                        case CollectibleType.Life:
                            other.GetComponent<PlayerHealth>().IncreaseMaxLives();
                            break;
                        case CollectibleType.Missile:
                            //Increase Missile Capacity.
                            break;
                        case CollectibleType.Bomb:
                            //Increase Bomb Capacity.
                            break;
                        default:
                            break;
                    }
                }
                _collected = true;
                UIManager.Instance.CollectibleTextUpdate(1);
                Destroy(this.gameObject);
            }
        }
    }
}
