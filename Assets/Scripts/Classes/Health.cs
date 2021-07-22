using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        [SerializeField] protected bool _isPlayer = false;
        [SerializeField] protected sbyte _hp;
        [SerializeField] protected sbyte _lives;
        [SerializeField] protected byte _maxLives;
        protected bool _gameStart = true;
        protected byte _maxLifeHP = 99;
        protected UIManager _uIManager;
        public sbyte HP { get { return _hp; } set { _hp = value; } }
        public bool IsPlayer { get { return _isPlayer;} }
        public sbyte Lives { get { return _lives; } private set { _lives = value; } }

        public virtual void Damage(sbyte hpDamage)
        {
            _hp -= hpDamage;
           
            if (_hp <= 0)
                Destroy(gameObject);
        }
        public virtual void Heal(sbyte amount)
        {
            _hp += amount;
            if(_hp + amount > _maxLifeHP && _lives == _maxLives)
            {
                _hp = (sbyte)_maxLifeHP;
            }
        }
    }
}


