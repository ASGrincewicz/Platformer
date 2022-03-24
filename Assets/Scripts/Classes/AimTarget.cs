// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class AimTarget : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 20.0f;
        [SerializeField] private GameObject _aimTargetObject;
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField] private Transform _belowTarget;
        [SerializeField] private Transform _centerAimTarget;
        [SerializeField] private Transform _downAimTarget;
        [SerializeField] private Transform _overHeadTarget;
        [SerializeField] private Transform _upAimTarget;
        private float _deltaTime;
        private float _vertical;
        private Transform _target;
        private Transform _targetPosition;
       

        private void OnEnable()
        {
            _inputManager.downAimAction += OnDownAimInput;
            _inputManager.moveAction += OnMoveInput;
            _inputManager.upAimAction += OnUpAimInput;
        }
        private void OnDisable()
        {
            _inputManager.downAimAction -= OnDownAimInput;
            _inputManager.moveAction -= OnMoveInput;
            _inputManager.upAimAction -= OnUpAimInput;
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _target = _targetPosition;
            if(_targetPosition != null)
            {
                SetTarget();
            }
          
        }
        private void SetTarget()
        {
            if (_targetPosition == _upAimTarget && _vertical > 0.5f)
                _target = _overHeadTarget;

            else if (_targetPosition == _downAimTarget && _vertical < -0.5f)
                _target = _belowTarget;

            else
                _target = _targetPosition;

            _aimTargetObject.transform.position = Vector3.Lerp(_aimTargetObject.transform.position, _target.position, _lerpSpeed * _deltaTime);
        }
       
        private void OnDownAimInput(float input)
        {
            _targetPosition = input > 0 ? _targetPosition = _downAimTarget : _targetPosition = _centerAimTarget;
        }

        private void OnMoveInput(float h, float v) => _vertical = v;

        private void OnUpAimInput(float input)
        {
            _targetPosition = input > 0 ? _targetPosition = _upAimTarget : _targetPosition = _centerAimTarget;
        }
    }
}