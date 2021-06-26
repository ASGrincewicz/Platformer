// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class MovingPlatform : Platform, IMoving
    {
        [SerializeField] private List<Transform> _waypoints = new List<Transform>();
        [SerializeField] private float _speed;
        [SerializeField] private Transform _playerParent;
        private int _targetWaypont;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_targetWaypont].position , _speed * Time.deltaTime);
            if (transform.position == _waypoints[_targetWaypont].position)
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