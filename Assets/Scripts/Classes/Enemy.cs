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
        private MeshRenderer _meshRenderer;
        [SerializeField] private int _hitPoints;
        private Health _health;
        private Vector3 _chaseDestination;
        private WaitForSeconds _chaseCoolDown;


        private void Start()
        {
            _health = GetComponent<Health>();
            _agent = GetComponentInChildren<NavMeshAgent>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _chaseCoolDown = new WaitForSeconds(3f);
            ChangeAIState(AIState.Patrolling);
        }
        private void Update()
        {
            Detect();
            if (_aiState == AIState.Patrolling)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                {
                    GoToNextPoint();
                }
            }
        }
        private void ChangeAIState(AIState state)
        {
            switch (state)
            {
                case AIState.Idle:
                    _meshRenderer.material.color = Color.gray;
                    break;
                case AIState.Patrolling:
                    _agent.enabled = true;
                    _meshRenderer.material.color = Color.blue;
                    _aiState = AIState.Patrolling;
                    break;
                case AIState.Chasing:
                    _agent.enabled = true;
                    _meshRenderer.material.color = Color.yellow;
                    _aiState = AIState.Chasing;
                    _agent.SetDestination(_chaseDestination);
                    break;
                case AIState.Attacking:
                    _agent.enabled = false;
                    _meshRenderer.material.color = Color.red;
                    _aiState = AIState.Attacking;
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
          
            if (Physics.Raycast(ray, out hitInfo, _sightDistance, _targetLayer))
            {
                if(hitInfo.collider != null)
                {
                    ChangeAIState(AIState.Chasing);
                    _chaseDestination = hitInfo.transform.position;
                    if (_aiState == AIState.Chasing)
                    {
                        if (Vector3.Distance(_agent.transform.position, _agent.destination) <= _attackRange)
                        {
                            ChangeAIState(AIState.Attacking);
                        }
                    }
                }
            }
            else if (hitInfo.collider == null && _aiState != AIState.Patrolling)
            {
                StartCoroutine(ChaseCoolDown());
            }
        }
        private IEnumerator ChaseCoolDown()
        {
            yield return _chaseCoolDown;
            ChangeAIState(AIState.Patrolling);
        }
    }
}
