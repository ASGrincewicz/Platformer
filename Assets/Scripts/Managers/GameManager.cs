using UnityEngine;

namespace Veganimus.Platformer
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private static GameManager _instance;
        #endregion
        [SerializeField] private GameObject _player;
        [SerializeField] private GameStateChannel _gameStateChannel;
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;
        [SerializeField] private Transform _spawnPoint;
        private bool _isGameOver;
        private bool _isPaused;
        private bool _isPlayerDead;
        private int _collectibles = 0;
        [SerializeField] private int _enemyKills = 0;
        private int _upgradesCollected = 0;
        private GameState _gameState;
        public int Collectibles { get { return _collectibles; } set { _collectibles = value; } }
        public int EnemyKills { get { return _enemyKills; } set { _enemyKills = value; } }
        public int UpgradesCollected { get { return _upgradesCollected; } set { _upgradesCollected = value; } }

        private void Awake() => _instance = this;

        private void OnEnable()
        {
            _gameStateChannel.OnGameStateChange.AddListener(ChangeGameState);
            _inputManager.pauseAction += OnPauseInput;
            _respawnPlayerChannel.OnPlayerDeath.AddListener(RespawnPlayer);
        }
        private void OnDisable()
        {
            _gameStateChannel.OnGameStateChange.RemoveListener(ChangeGameState);
            _inputManager.pauseAction -= OnPauseInput;
            _respawnPlayerChannel.OnPlayerDeath.RemoveListener(RespawnPlayer);
            
        }
        private void Start() => ChangeGameState(GameState.Start);

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
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
                case GameState.Finish:
                    //Display Win UI
                    Debug.Log("Level Complete!");
                    break;
                default:
                    break;
            }
        }
       
        private void RespawnPlayer()
        {
            if (!_isGameOver)
            {
                _player.transform.position = _spawnPoint.position;
                _player.transform.rotation = Quaternion.identity;
            }
        }

        public void OnPauseInput(bool isPaused)
        {
            //var isPaused = _inputManager.controls.Standard.Pause.triggered;
            if (isPaused && !_isPaused)
            {
                _isPaused = true;
                Time.timeScale = 0;
            }
            else if (isPaused && _isPaused)
            {
                _isPaused = false;
                Time.timeScale = 1;
            }
            UIManager.Instance.ActivatePauseMenu(_isPaused);
        }
    }
}