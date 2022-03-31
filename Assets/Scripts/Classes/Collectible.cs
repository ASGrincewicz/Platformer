// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    /// <summary>
    /// This class should be used with any object the player can collect.
    /// </summary>
    public class Collectible : MonoBehaviour, IAbsorbable
    {
        [SerializeField, Tooltip("Can this collectible be absorbed?")]
        private bool _canAbsorb = false;
        [SerializeField, Tooltip("Specify the amount this collectible adds to a stat.")]
        private sbyte _powerUpAmount;
        [SerializeField, Tooltip("This object's movement speed.")]
        private float _speed;
        [SerializeField, Tooltip("Specify the type of collectible.")]
        protected CollectibleType _collectibleType;
        [SerializeField,Tooltip("Allows the collectible to be collected only by objects on the specified layer mask")]
        protected LayerMask _collectorLayerMask;
        protected bool _collected;
        private float _time = 0;
        private float _x, _y, _z = 0;
        private float _xRadius, _yRadius;
        protected Transform _transform;
        public bool CanAbsorb => _canAbsorb;

        protected virtual void Start()
        {
            _transform = transform;
            _xRadius = Random.Range(-0.05f, 0.05f);
            _yRadius = Random.Range(-0.05f, 0.05f);
        }

        protected virtual void Update()
        {
            if (Time.timeScale > 0)
            {
                _time += GameManager.DeltaTime;
                _x = _xRadius * Mathf.Cos(_time * _speed);
                _y = _yRadius * Mathf.Sin(_time * _speed);
                _transform.position += new Vector3(_x, _y, _z);
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if(_canAbsorb)
                {
                    switch(_collectibleType)
                    {
                        case CollectibleType.Health:
                            other.GetComponentInParent<PlayerHealth>().Heal((sbyte)(_powerUpAmount / 2));
                            break;
                        case CollectibleType.Life:
                            other.GetComponentInParent<PlayerHealth>().IncreaseMaxLives();
                            break;
                        case CollectibleType.Missile:
                            //Increase Missile Capacity.
                            break;
                        case CollectibleType.Bomb:
                            //Increase Bomb Capacity.
                            break;
                        case CollectibleType.Upgrade:
                            //enable upgrade
                            break;
                        default:
                            break;
                    }
                }
                _collected = true;
                UIManager.Instance.CollectibleTextUpdate(1);
                GameManager.Instance.Collectibles++;
                Destroy(gameObject);
            }
        }
    }
}
