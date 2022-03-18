using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        [SerializeField, Tooltip("Indicate if object is the player.")]
        protected bool _isPlayer = false;
        [SerializeField, Tooltip("Hit Points")]
        protected int _hp;
        [SerializeField, Tooltip("Lives and/or Energy Tanks")]
        protected int _lives;
        [SerializeField, Tooltip("Max Lives and/or Energy Tanks")]
        protected int _maxLives;
        [SerializeField, Tooltip("Set Friendliness.")]
        protected CharacterType _characterType;
        protected bool _gameStart = true;
        protected int _maxLifeHP = 99;
        protected UIManager _uIManager;
        public int HP { get { return _hp; } set { _hp = value; } }
        public bool IsPlayer { get { return _isPlayer;} }
        
        public int Lives { get { return _lives; } private set { _lives = value; } }

        public virtual void Damage(int hpDamage)
        {
            _hp -= hpDamage;

            if (_hp <= 0 && _characterType == CharacterType.Enemy)
            {
                
                Destroy(gameObject);
            }
            else if(_hp <= 0 && _characterType != CharacterType.Enemy)
                Destroy(gameObject);
        }
        /// <summary>
        /// Pass in the <value>amount to heal the implementing object.</value>
        /// </summary>
        /// <param name="amount"></param>
        public virtual void Heal(int amount)
        {
            _hp += amount;
            if(_hp + amount > _maxLifeHP && _lives == _maxLives)
            {
                _hp = (int)_maxLifeHP;
            }
        }
    }
}


