using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Veganimus.Platformer
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private byte _destinationPoint;
        [SerializeField] private AIState _aiState;
        [SerializeField] private EnemyInfo _enemyInfo;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] EnemyWeapon _weapon;
        [SerializeField] Transform[] _navPoints;
        private Color _enemyColor;
        private RaycastHit _hitInfo;
        private Vector3 _chaseDestination;
        private Health _health;
        private NavMeshAgent _agent;
        private Transform _agentTransform;
        private WaitForSeconds _chaseCoolDown;
        public Color currentColor;

        private void Start()
        {
            _health = GetComponent<Health>();
            _health.HP = _enemyInfo.hitPoints;
            _agent = GetComponentInChildren<NavMeshAgent>();
            _enemyColor = GetComponentInChildren<MeshRenderer>().material.color;
            _weapon = GetComponent<EnemyWeapon>();
            _chaseCoolDown = new WaitForSeconds(3f);
            _agent.speed = _enemyInfo.speed;
            _agentTransform = _agent.transform;
            ChangeAIState(AIState.Patrolling);
        }
        private void FixedUpdate()
        {
            Detect();
            if (_aiState == AIState.Patrolling)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                    GoToNextPoint();
            }
            if(_aiState == AIState.Attacking)
            {
                if(_weapon != null)
                    _weapon.IsShooting = true;
            }
            else
                _weapon.IsShooting = false;
        }
        private void ChangeAIState(AIState state)
        {
            switch (state)
            {
                case AIState.Idle:
                    _enemyColor = Color.gray;
                    currentColor = Color.gray;
                    break;
                case AIState.Patrolling:
                    _agent.isStopped = false;
                    _agent.speed = _enemyInfo.speed;
                    _enemyColor = Color.blue;
                    currentColor = Color.blue;
                    _aiState = AIState.Patrolling;
                    break;
                case AIState.Chasing:
                    _agent.isStopped = false;
                    _enemyColor = Color.yellow;
                    currentColor = Color.yellow;
                    _aiState = AIState.Chasing;
                    _agent.SetDestination(_chaseDestination);
                    _agent.speed = _enemyInfo.chaseSpeed;
                    break;
                case AIState.Attacking:
                    _agent.isStopped = true;
                    _enemyColor = Color.red;
                    currentColor = Color.red;
                    _aiState = AIState.Attacking;
                    break;
                case AIState.Stunned:
                    _agent.isStopped = true;
                    StartCoroutine(ChaseCoolDown());
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
                _destinationPoint = (byte)((_destinationPoint + 1) % _navPoints.Length);
            }
        }
        private void Detect()
        {
            Ray ray = new Ray(_agentTransform.position, _agentTransform.forward);

            if (Physics.Raycast(ray, out _hitInfo, _enemyInfo.sightDistance, _targetLayer))
            {
                if (_hitInfo.collider != null)
                {
                    ChangeAIState(AIState.Chasing);
                    _chaseDestination = _hitInfo.transform.position;
                    if (_aiState == AIState.Chasing)
                    {
                        if (Vector3.Distance(_agent.transform.position, _agent.destination) <= _enemyInfo.attackRange)
                            ChangeAIState(AIState.Attacking);
                    }
                }
            }
            else if (_hitInfo.collider == null && _aiState != AIState.Patrolling)
                StartCoroutine(ChaseCoolDown());
            
        }
       
        private IEnumerator ChaseCoolDown()
        {
            yield return _chaseCoolDown;
            ChangeAIState(AIState.Patrolling);
            GoToNextPoint();
        }
    }
}
