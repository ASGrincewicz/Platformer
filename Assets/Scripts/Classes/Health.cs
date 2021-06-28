using System.Collections;
using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        [SerializeField] private int _maxLives;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private bool _isPlayer;
        public bool IsPlayer { get { return _isPlayer; } }
        [SerializeField] private int _lives;
        public int Lives { get { return _lives; }private set { _lives = value; } }
        [SerializeField] private int _hp;
        public int HP { get { return _hp; }set { _hp = value; } }
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private GameStateChannel _gameStateChannel;

        private IEnumerator Start()
        {
            if (_characterType == CharacterType.Player)
            {
                yield return new WaitForSeconds(0.5f);
               Heal(0);
            }
        }
        private void Update()
        {
            if (_characterType == CharacterType.Player)
            {
                if (_hp > 99 && _lives != _maxLives)
                {
                    var amount = _hp - 100;
                    _hp = amount;
                    _lives++;
                    UIManagerUpdate();
                }
                
                if (_hp <= 0 && _lives > 0)
                {
                    _lives--;
                    var amount = 99 + _hp;
                    _hp = amount;
                    UIManagerUpdate();
                }
            }
        }
        private void UIManagerUpdate()
        {
            UIManager.Instance.HealthTextUpdate(_hp);
            UIManager.Instance.LivesUpdate(_lives);
        }
        public void Heal(int amount)
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
            _lives = _maxLives;
            _hp = 99;
            UIManagerUpdate();
        }
        public void Damage(int hpDamage)
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


