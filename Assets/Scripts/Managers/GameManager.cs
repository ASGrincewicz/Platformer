using UnityEngine;

namespace Veganimus.Platformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private GameStateChannel _gameStateChannel;
        private bool _isPlayerDead;
        private bool _isGameOver;
        private GameState _gameState;

        private void OnEnable()
        {
            _respawnPlayerChannel.OnPlayerDeath.AddListener(RespawnPlayer);
            _gameStateChannel.OnGameStateChange.AddListener(ChangeGameState);
        }
        private void OnDisable()
        {
            _respawnPlayerChannel.OnPlayerDeath.RemoveListener(RespawnPlayer);
            _gameStateChannel.OnGameStateChange.RemoveListener(ChangeGameState);
        }
        private void Start()
        {
            ChangeGameState(GameState.Start);
        }
        private void RespawnPlayer()
        {
            if (!_isGameOver)
            {
                _player.transform.position = _spawnPoint.position;
                _player.transform.rotation = Quaternion.identity;
            }
        }
        private void ChangeGameState(GameState gameState)
        {
            switch(gameState)
            {
                case GameState.Start:
                    _isGameOver = false;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    break;
                case GameState.GameOver:
                    _isGameOver = true;
                    break;
                default:
                    break;
            }
        }
    }
}


