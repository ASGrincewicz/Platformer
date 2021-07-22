// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class MovingPlatform : Platform, IMoving
    {
        [SerializeField] private float _speed;
        [SerializeField] private List<Transform> _waypoints = new List<Transform>();
        [SerializeField] private Transform _playerParent;
        private byte _targetWaypont;
        private float _deltaTime;
        private Transform _transform;

        private void Start() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_targetWaypont].position , _speed * _deltaTime);
            if (_transform.position == _waypoints[_targetWaypont].position)
                MoveToWayPoint();
        }
        private void MoveToWayPoint()
        {
            if (_targetWaypont != _waypoints.Count - 1 || _targetWaypont! > _waypoints.Count)
                _targetWaypont++;

            else
                _targetWaypont = 0;
        }
    }
}