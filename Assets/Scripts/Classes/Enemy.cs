using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Veganimus.Platformer
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private AIState _aiState;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _sightDistance;
        [SerializeField] private float _attackRange;
        [SerializeField] private int _destinationPoint;
        [SerializeField] Transform[] _navPoints;
        
        private NavMeshAgent _agent;
        [SerializeField] private int _hitPoints;
        private Health _health;
        private Vector3 _chaseDestination;


        private void Start()
        {
            _health = GetComponent<Health>();
            _agent = GetComponentInChildren<NavMeshAgent>();
            _aiState = AIState.Patrolling;
            if(_aiState == AIState.Patrolling)
            GoToNextPoint();
        }
        private void Update()
        {
            Detect();
            if(_aiState == AIState.Patrolling)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                {
                    GoToNextPoint();
                }
            }
            if(_aiState == AIState.Chasing)
            {
                if(_agent.remainingDistance > _sightDistance)
                {
                    ChangeAIState(AIState.Patrolling);
                }
                else if(_agent.remainingDistance <= _attackRange)
                {
                    ChangeAIState(AIState.Attacking);
                }
            }
           
            
        }
        private void ChangeAIState(AIState state)
        {
            switch (state)
            {
                case AIState.Idle:
                    break;
                case AIState.Patrolling:
                    _aiState = AIState.Patrolling;
                    GoToNextPoint();
                    break;
                case AIState.Chasing:
                    _aiState = AIState.Chasing;
                    _agent.SetDestination(_chaseDestination);
                    break;
                case AIState.Attacking:
                    break;
                case AIState.Dead:
                    break;
                default:
                    break;
            }
        }
        private void GoToNextPoint()
        {
            if (_navPoints.Length == 0)
                return;
            else
            {
                _agent.destination = _navPoints[_destinationPoint].position;
                _destinationPoint = (_destinationPoint + 1) % _navPoints.Length;
            }
        }
        private void Detect()
        {
            Ray ray = new Ray(_agent.transform.position, _agent.transform.forward);
            RaycastHit hitInfo;
            Debug.DrawRay(_agent.transform.position, _agent.transform.forward, Color.red, 3f, false);
            if (Physics.Raycast(ray, out hitInfo, _sightDistance, _targetLayer))
            {
                Debug.Log($"{hitInfo.collider.name} was found");
                ChangeAIState(AIState.Chasing);
                _chaseDestination = hitInfo.transform.position;
                
            }
            else
            {

                Debug.Log($"Target not found");
                
            }
                
        }
        private void OnCollisionEnter(Collision collision)
        {
           
        }
    }
}
