using UnityEngine;
namespace Veganimus.Platformer
{
    public class AreaBoundary : MonoBehaviour
    {
        private CameraController _mainCamera;

        //Camera Boundaries
        [SerializeField] private Transform _cameraStartPos;
        [SerializeField] private float _leftBounds;
        [SerializeField] private float _rightBounds;
        [SerializeField] private float _topBounds;
        [SerializeField] private float _bottonBounds;

        private void Start()
        {
            _mainCamera = Camera.main.GetComponent<CameraController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                _mainCamera.MoveCamera(_cameraStartPos.position, 7f);
                _mainCamera.ActivateLimits(_leftBounds, _rightBounds, _bottonBounds, _topBounds);
            }
        }
    }
}