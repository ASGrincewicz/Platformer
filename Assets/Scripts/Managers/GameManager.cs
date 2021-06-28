using UnityEngine;

namespace Veganimus.Platformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private GameStateChannel _gameStateChannel;
        [SerializeField] private InputManagerSO _inputManager;
        private bool _isPlayerDead;
        private bool _isGameOver;
        private bool _isPaused;
        private GameState _gameState;

        private void OnEnable()
        {
            _respawnPlayerChannel.OnPlayerDeath.AddListener(RespawnPlayer);
            _gameStateChannel.OnGameStateChange.AddListener(ChangeGameState);
            _inputManager.pauseAction += OnPauseInput;
        }
        private void OnDisable()
        {
            _respawnPlayerChannel.OnPlayerDeath.RemoveListener(RespawnPlayer);
            _gameStateChannel.OnGameStateChange.RemoveListener(ChangeGameState);
            _inputManager.pauseAction -= OnPauseInput;
        }
        private void Start()
        {
            ChangeGameState(GameState.Start);
        }
        public void OnPauseInput(bool isPaused)
        {
            //var isPaused = _inputManager.controls.Standard.Pause.triggered;
            if (isPaused == true && _isPaused == false)
            {
                _isPaused = true;
                Time.timeScale = 0;
            }
            else if(isPaused == true && _isPaused == true)
            {
                _isPaused = false;
                Time.timeScale = 1;
            }
            UIManager.Instance.ActivatePauseMenu(_isPaused);
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