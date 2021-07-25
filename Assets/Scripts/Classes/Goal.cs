// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private int _collectiblesRequired;
        [SerializeField] private int _enemyKillsRequired;
        [SerializeField] private int _upgradesRequired;
        [SerializeField] private GameStateChannel _gameStateChannel;

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