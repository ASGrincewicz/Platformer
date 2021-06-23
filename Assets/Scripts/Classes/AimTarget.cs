// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class AimTarget : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 20.0f;
        [SerializeField] private GameObject _aimTargetObject;
        [SerializeField] private Transform _upAimTarget;
        [SerializeField] private Transform _downAimTarget;
        [SerializeField] private Transform _centerAimTarget;
        [SerializeField] private Transform _overHeadTarget;
        [SerializeField] private Transform _belowTarget;
        [SerializeField] private InputManager _inputManager;
        private Transform _targetPosition;
        [SerializeField] private float _vertical;

        private void OnEnable()
        {
            _inputManager.upAimAction += OnUpAimInput;
            _inputManager.downAimAction += OnDownAimInput;
            _inputManager.moveAction += OnMoveInput;
        }
        private void OnDisable()
        {
            _inputManager.upAimAction -= OnUpAimInput;
            _inputManager.downAimAction -= OnDownAimInput;
            _inputManager.moveAction -= OnMoveInput;
        }

        private void Update()
        {

            Transform target = _targetPosition;
            if(_targetPosition != null)
            {
                if(_targetPosition == _upAimTarget && _vertical > 0.5f)
                {
                    target = _overHeadTarget;
                }
                else if(_targetPosition == _downAimTarget && _vertical < -0.5f)
                {
                    target = _belowTarget;
                }
                else
                {
                    target = _targetPosition;
                }
                _aimTargetObject.transform.position = Vector3.Lerp(_aimTargetObject.transform.position, target.position, _lerpSpeed * Time.deltaTime);
            }
          
        }
        private void OnUpAimInput(float input)
        {
            if (input > 0)
                _targetPosition = _upAimTarget;
            
            else
                _targetPosition = _centerAimTarget;

        }
        private void OnDownAimInput(float input)
        {
            if (input > 0)
                _targetPosition = _downAimTarget;
            else
                _targetPosition = _centerAimTarget;
        }
        private void OnMoveInput(float h, float v)
        {
            _vertical = v;
        }
    }
}