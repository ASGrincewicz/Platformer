using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Veganimus.Platformer
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _destinationPoint;
        [SerializeField] private AIState _aiState;
        [SerializeField] private EnemyInfo _enemyInfo;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] EnemyWeapon _weapon;
        [SerializeField] Transform[] _navPoints;
        private readonly int _attackingAP = Animator.StringToHash("Attacking");
        private readonly int _chasingAP = Animator.StringToHash("Chasing");
        private readonly int _deadAP = Animator.StringToHash("Dead");
        private readonly int _patrollingAP = Animator.StringToHash("Patrolling");
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _chaseDestination;
        private Animator _animator;
        private Health _health;
        private NavMeshAgent _agent;
        private Transform _agentTransform;
        private WaitForSeconds _chaseCoolDown;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _health = GetComponent<Health>();
            _health.HP = _enemyInfo.hitPoints;
            _agent = GetComponentInChildren<NavMeshAgent>();
            _weapon = GetComponent<EnemyWeapon>();
            _chaseCoolDown = new WaitForSeconds(3f);
            _agent.speed = _enemyInfo.speed;
            _agentTransform = _agent.transform;
            ChangeAIState(AIState.Patrolling);
        }
        private void OnDestroy()
        {
            GameManager.Instance.EnemyKills++;
        }

        private void FixedUpdate()
        {
            Detect();
        }
        private void Update()
        {
            //Detect();
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
                   
                    break;
                case AIState.Patrolling:
                    _agent.isStopped = false;
                    _agent.speed = _enemyInfo.speed;
                    _animator.SetBool(_attackingAP, false);
                    _animator.SetBool(_chasingAP, false);
                    _animator.SetBool(_patrollingAP, true);
                    _aiState = AIState.Patrolling;
                    break;
                case AIState.Chasing:
                    _agent.isStopped = false;
                    _animator.SetBool(_chasingAP, true);
                    _animator.SetBool(_patrollingAP, false);
                    _animator.SetBool(_attackingAP, false);
                    _aiState = AIState.Chasing;
                    _agent.SetDestination(_chaseDestination);
                    _agent.speed = _enemyInfo.chaseSpeed;
                    break;
                case AIState.Attacking:
                    _agent.isStopped = true;
                    _animator.SetBool(_chasingAP, false);
                    _animator.SetBool(_patrollingAP, false);
                    _animator.SetBool(_attackingAP, true);
                    _aiState = AIState.Attacking;
                    break;
                case AIState.Stunned:
                    _agent.isStopped = true;
                    StartCoroutine(ChaseCoolDown());
                    break;
                case AIState.Dead:
                    _animator.SetBool(_deadAP, true);
                    GameManager.Instance.EnemyKills++;
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
            _ray = new Ray(_agentTransform.position, _agentTransform.forward);

            if (Physics.SphereCast(_ray, 1f,out _hitInfo, _enemyInfo.sightDistance, _targetLayer))
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
