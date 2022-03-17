// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Goal : MonoBehaviour
    {
        [SerializeField, Tooltip("Amount of collectibles required to meet win condition.")]
        private int _collectiblesRequired;
        [SerializeField, Tooltip("Amount of enemy kills required to meet win condition.")]
        private int _enemyKillsRequired;
        [SerializeField, Tooltip("Amount of upgrades required to meet win condition.")]
        private int _upgradesRequired;
        [SerializeField] private GameStateChannel _gameStateChannel;

        /// <summary>
        /// Determines whether the player meets the win conditions
        /// at the time the goal was reached.
        /// </summary>
        public void CheckWinConditions()
        {
            if (GameManager.Instance.Collectibles == _collectiblesRequired
            && GameManager.Instance.EnemyKills == _enemyKillsRequired
            && GameManager.Instance.UpgradesCollected == _upgradesRequired)
            {
                _gameStateChannel.RaiseGameStateChange(GameState.Finish);
            }
            else
            {
                Debug.Log(GameManager.Instance.UpgradesCollected);
                Debug.Log(GameManager.Instance.EnemyKills);
                Debug.Log(GameManager.Instance.Collectibles);
            }

        }
    }
}