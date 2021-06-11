using UnityEngine;
using UnityEngine.Events;
namespace Veganimus.Platformer
{
    [CreateAssetMenu(menuName = "Game State Channel")]
    public class GameStateChannel : ScriptableObject
    {
        public UnityEvent<GameState> OnGameStateChange;

        public void RaiseGameStateChange(GameState gameState)
        {
            if (OnGameStateChange != null)
                OnGameStateChange.Invoke(gameState);
        }
    }
}


