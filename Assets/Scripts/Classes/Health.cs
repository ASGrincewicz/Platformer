using System.Collections;
using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        private bool _gameStart = true;
        [SerializeField] private byte _maxLives;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private bool _isPlayer;
        public bool IsPlayer { get { return _isPlayer; } }
        [SerializeField] private sbyte _lives;
        public sbyte Lives { get { return _lives; }private set { _lives = value; } }
        [SerializeField] private sbyte _hp;
        public sbyte HP { get { return _hp; }set { _hp = value; } }
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private GameStateChannel _gameStateChannel;

        private IEnumerator Start()
        {
            if (_characterType == CharacterType.Player)
            {
                yield return new WaitForSeconds(0.5f);
                Heal(0);
                _gameStart = false;
            }
        }
        private void Update()
        {
            if (_characterType == CharacterType.Player)
            {
                if (_hp > 99 && _lives != _maxLives)
                {
                    sbyte amount = (sbyte)(_hp - 100);
                    _hp = amount;
                    _lives++;
                    UIManagerUpdate();
                }
                
                if (_hp <= 0 && _lives > 1)
                {
                    _lives--;
                    sbyte amount = (sbyte)(99 + _hp);
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
            UIManager.Instance.HealthTextUpdate(_hp);
            UIManager.Instance.LivesUpdate(_lives);
        }
        public void Heal(sbyte amount)
        {
            _hp += amount;
            if(_hp + amount > 99 && _lives == _maxLives)
            {
                _hp = 99;
            }
            UIManagerUpdate();
        }
        public void IncreaseMaxLives()
        {
            _maxLives += 1;
            _lives = (sbyte)_maxLives;
            _hp = 99;
            UIManagerUpdate();
        }
        public void Damage(sbyte hpDamage)
        {
            _hp -= hpDamage;
            switch(_characterType)
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
    }
}


