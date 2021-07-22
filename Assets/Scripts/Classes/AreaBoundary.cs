using UnityEngine;
namespace Veganimus.Platformer
{
    public class AreaBoundary : MonoBehaviour
    {
        //Camera Boundaries
        [SerializeField] private byte _areaID;
        [SerializeField] private float _bottonBounds;
        [SerializeField] private float _leftBounds;
        [SerializeField] private float _rightBounds;
        [SerializeField] private float _topBounds;
        [SerializeField] private Transform _cameraStartPos;
        private CameraController _mainCamera;

        private void Start() => _mainCamera = Camera.main.GetComponent<CameraController>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && _mainCamera.AreaID != _areaID)
            {
                _mainCamera.MoveCamera(_cameraStartPos.position, 7f);
                _mainCamera.ActivateLimits(_areaID, _leftBounds, _rightBounds, _bottonBounds, _topBounds);
            }
            else
                return;
        }
    }
}