using System.Collections;
using UnityEngine;

namespace Veganimus.Platformer
{
    public class PlayerHealth : Health
    {
        [SerializeField] private GameStateChannel _gameStateChannel;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;

        private IEnumerator Start() 
        {
                yield return new WaitForSeconds(0.5f);
                _uIManager = UIManager.Instance;
                Heal(0);
                _gameStart = false;
        }

        private void Update()
        {
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (_hp > _maxLifeHP && _lives != _maxLives)
            {
                int amount = _hp - 100;
                _hp = amount;
                _lives++;
                UIManagerUpdate();
            }

            if (_hp <= 0 && _lives > 1)
            {
                _lives--;
                int amount = _maxLifeHP + _hp;
                _hp = amount;
                UIManagerUpdate();
            }
            else if (_hp <= 0 && _lives <= 1 && !_gameStart)
            {
                Destroy(gameObject);
            }
        }

        private void UIManagerUpdate()
        {
            _uIManager.HealthTextUpdate(_hp);
            _uIManager.LivesUpdate(_lives);
        }

        public override void Damage(int hpDamage)
        {
            _hp -= hpDamage;
            UIManagerUpdate();
        }

        public override void Heal(int amount)
        {
            base.Heal(amount);
            UIManagerUpdate();
        }
        /// <summary>
        /// Increase the object's max life count.
        /// </summary>
        public void IncreaseMaxLives()
        {
            _maxLives += 1;
            _lives = _maxLives;
            _hp = _maxLifeHP;
            UIManagerUpdate();
        }
    }
}