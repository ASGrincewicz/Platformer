using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.Platformer
{
    [CreateAssetMenu(menuName = "Respawn Channel")]
    public class RespawnPlayerChannel : ScriptableObject
    {
        public UnityEvent OnPlayerDeath;

        public void RaiseRespawnEvent()
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath.Invoke();
        }

    }
}


