using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Veganimus.Platformer
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyInfo _enemyInfo;
        [SerializeField] private AIState _aiState;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private byte _destinationPoint;
        [SerializeField] Transform[] _navPoints;
        [SerializeField] EnemyWeapon _weapon;
        private NavMeshAgent _agent;
        private MeshRenderer _meshRenderer;
        private Health _health;
        private Vector3 _chaseDestination;
        public Color currentColor;
        private WaitForSeconds _chaseCoolDown;

        private void Start()
        {
            _health = GetComponent<Health>();
            _health.HP = _enemyInfo.hitPoints;
            _agent = GetComponentInChildren<NavMeshAgent>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _weapon = GetComponent<EnemyWeapon>();
            _chaseCoolDown = new WaitForSeconds(3f);
            _agent.speed = _enemyInfo.speed;
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
                    _weapon.isShooting = true;
            }
            else
                _weapon.isShooting = false;
        }
        private void ChangeAIState(AIState state)
        {
            switch (state)
            {
                case AIState.Idle:
                    _meshRenderer.material.color = Color.gray;
                    currentColor = Color.gray;
                    break;
                case AIState.Patrolling:
                    _agent.isStopped = false;
                    _agent.speed = _enemyInfo.speed;
                    _meshRenderer.material.color = Color.blue;
                    currentColor = Color.blue;
                    _aiState = AIState.Patrolling;
                    break;
                case AIState.Chasing:
                    _agent.isStopped = false;
                    _meshRenderer.material.color = Color.yellow;
                    currentColor = Color.yellow;
                    _aiState = AIState.Chasing;
                    _agent.SetDestination(_chaseDestination);
                    _agent.speed = _enemyInfo.chaseSpeed;
                    break;
                case AIState.Attacking:
                    _agent.isStopped = true;
                    _meshRenderer.material.color = Color.red;
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
            Ray ray = new Ray(_agent.transform.position, _agent.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _enemyInfo.sightDistance, _targetLayer))
            {
                if (hitInfo.collider != null)
                {
                    ChangeAIState(AIState.Chasing);
                    _chaseDestination = hitInfo.transform.position;
                    if (_aiState == AIState.Chasing)
                    {
                        if (Vector3.Distance(_agent.transform.position, _agent.destination) <= _enemyInfo.attackRange)
                            ChangeAIState(AIState.Attacking);
                    }
                }
            }
            else if (hitInfo.collider == null && _aiState != AIState.Patrolling)
                StartCoroutine(ChaseCoolDown());
            
        }
       
        private IEnumerator ChaseCoolDown()
        {
            yield return _chaseCoolDown;
            ChangeAIState(AIState.Patrolling);
        }
    }
}
