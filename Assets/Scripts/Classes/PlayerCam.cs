using UnityEngine;

namespace Veganimus.Platformer
{
    public class PlayerCam : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPos;
        [SerializeField] private RespawnPlayerChannel _respawnPlayerChannel;

        private void OnEnable()
        {
            _respawnPlayerChannel.OnPlayerDeath.AddListener(ResetCamera);
        }
        private void OnDisable()
        {
            _respawnPlayerChannel.OnPlayerDeath.RemoveListener(ResetCamera);
        }
        private void Start()
        {
            _startPos = transform.position;
        }
        private void ResetCamera()
        {
            transform.position = _startPos;
        }
    }
}


