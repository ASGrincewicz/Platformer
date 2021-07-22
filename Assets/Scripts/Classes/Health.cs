using System.Collections;
using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        [SerializeField] private bool _isPlayer;
        [SerializeField] private sbyte _hp;
        [SerializeField] private sbyte _lives;
        [SerializeField] private byte _maxLives;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private GameStateChannel _gameStateChannel;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        private bool _gameStart = true;
        private byte _maxLifeHP = 99;
        private UIManager _uIManager;
        public bool IsPlayer { get { return _isPlayer; } }
        public sbyte HP { get { return _hp; } set { _hp = value; } }
        public sbyte Lives { get { return _lives; }private set { _lives = value; } }
       

        private IEnumerator Start()
        {
            if (_characterType == CharacterType.Player)
            {
                yield return new WaitForSeconds(0.5f);
                _uIManager = UIManager.Instance;
                Heal(0);
                _gameStart = false;
            }
        }
        private void Update()
        {
            if (_characterType == CharacterType.Player)
            {
                if (_hp > _maxLifeHP && _lives != _maxLives)
                {
                    sbyte amount = (sbyte)(_hp - 100);
                    _hp = amount;
                    _lives++;
                    UIManagerUpdate();
                }
                
                if (_hp <= 0 && _lives > 1)
                {
                    _lives--;
                    sbyte amount = (sbyte)(_maxLifeHP + _hp);
                    _hp = amount;
                    UIManagerUpdate();
                }
                else if(_hp <=0 && _lives <= 1 && !_gameStart)
                {
                    Destroy(gameObject);
                }
            }
        }
        private void UIManagerUpdate()
        {
            _uIManager.HealthTextUpdate(_hp);
            _uIManager.LivesUpdate(_lives);
        }

        public void Damage(sbyte hpDamage)
        {
            _hp -= hpDamage;
            switch (_characterType)
            {
                case CharacterType.Player:
                    UIManagerUpdate();
                    break;
                case CharacterType.Enemy:
                    break;
                default:
                    break;
            }
            if (_hp <= 0 && _characterType != CharacterType.Player)
                Destroy(gameObject);
        }
        public void Heal(sbyte amount)
        {
            _hp += amount;
            if(_hp + amount > _maxLifeHP && _lives == _maxLives)
            {
                _hp = (sbyte)_maxLifeHP;
            }
            UIManagerUpdate();
        }
        public void IncreaseMaxLives()
        {
            _maxLives += 1;
            _lives = (sbyte)_maxLives;
            _hp = (sbyte)_maxLifeHP;
            UIManagerUpdate();
        }
    }
}


