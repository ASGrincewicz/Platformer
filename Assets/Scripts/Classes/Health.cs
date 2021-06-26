using UnityEngine;

namespace Veganimus.Platformer
{
    public class Health : MonoBehaviour, IDamageable, IBombable
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private bool _isPlayer;
        public bool IsPlayer { get { return _isPlayer; } }
        [SerializeField] private int _lives;
        public int Lives { get { return _lives; }private set { _lives = value; } }
        [SerializeField] private int _hp;
        public int HP { get { return _hp; }private set { _hp = value; } }
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private GameStateChannel _gameStateChannel;

        public void Damage(int hpDamage)
        {
            _hp -= hpDamage;
           
            if (_hp <= 0)
            {
                _hp = 0;
                if (_characterType == CharacterType.Player)
                {
                    _lives--;

                    if (_lives > 0 || hpDamage == 9999)
                    {
                        _respawnPlayerChannel.RaiseRespawnEvent();
                    }
                    else
                    {
                        _gameStateChannel.RaiseGameStateChange(GameState.GameOver);
                        Destroy(this.gameObject);
                    }
                }
                
                Destroy(this.gameObject);
                //game over
            }
        }
    }
}


