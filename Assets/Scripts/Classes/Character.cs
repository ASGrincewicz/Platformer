// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour
    {
        #region Singleton
        public static Character Instance { get { return _instance; } }
        private static Character _instance;
        #endregion
        [SerializeField] private int _collectibles;
        [SerializeField] private float _adjustGravity;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight = 15.0f;
        [SerializeField] private float _wallJumpBounceOffModifier = 1.5f;
        [SerializeField] private int _wallJumpCount = 0;
        [SerializeField] private int _wallJumpLimit = 1;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _yVelocityDownLimit = -20.0f;
        [SerializeField] private float _yVelocityUpLimit = 10.0f;
        [SerializeField] private float _ballModeXVelocityLimitRight = 10.0f;
        [SerializeField] private float _ballModeXVelocityLimitLeft = -10.0f;
        [SerializeField] private PlayerUpgrades _upgrades;
        [SerializeField] private Vector3 _modelPosition;
        [SerializeField] private CameraController _mainCamera;
        [SerializeField] private GameObject _aimTarget;
        [SerializeField] private GameObject _ballForm;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private InputManagerSO _inputManager;
        private bool _ballModeTriggered;
        private bool _canDoubleJump;
        private bool _canWallJump;
        private bool _grabbingLedge;
        private bool _jumpTriggered;
        private bool _inBallForm;
        private bool _isCrouching;
        private bool _isHanging;
        private bool _isWallJumping;
        //Animator Parameters
        private readonly int _bracedAP = Animator.StringToHash("braced");
        private readonly int _crouchAP = Animator.StringToHash("crouch");
        private readonly int _droppingAP = Animator.StringToHash("dropping");
        private readonly int _fallingAP = Animator.StringToHash("falling");
        private readonly int _grabLedgeAP = Animator.StringToHash("grabLedge");
        private readonly int _groundedAP = Animator.StringToHash("grounded");
        private readonly int _hangingAP = Animator.StringToHash("hanging");
        private readonly int _horizontalAP = Animator.StringToHash("horizontal");
        private readonly int _jumpingAP = Animator.StringToHash("jumping");
        private readonly int _wallJumpingAP = Animator.StringToHash("wallJumping");
        private float _aimTargetCrouchingPos = 0.85f;
        private float _aimTargetStandingPos = 1.4f;
        private float _horizontal;
        private float _vertical;
        private float _z = 0;
        private float _yVelocity;
        private RaycastHit _hitInfo;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        private Animator _animator;
        private CharacterController _controller;
        private Ledge _activeLedge;
        private PlayerAim _playerAim;
        private Rigidbody _rigidbody;
        private Transform _aimTransform;
        private Transform _characterModelTransform;
        private Transform _ballFormTransform;
        private Transform _transform;
        private Weapon _weapon;
        public bool InBallForm { get { return _inBallForm; } }
        private float _deltaTime;
        public PlayerUpgrades Upgrades { get { return _upgrades; } }
        public InputManagerSO InputManager { get; set; }

        private void Awake() => _instance = this;

        private void OnEnable()
        {
            _inputManager.moveAction += OnMoveInput;
            _inputManager.crouchAction += OnCrouchInput;
        }
        private void OnDisable()
        {
            _inputManager.moveAction -= OnMoveInput;
            _inputManager.crouchAction -= OnCrouchInput;
        }

        private void Start()
        {
            _transform = transform;
            _characterModelTransform = _characterModel.transform;
            _ballFormTransform = _ballForm.transform;
            _aimTransform = _aimTarget.transform;
            _mainCamera = Camera.main.GetComponent<CameraController>();
            _controller = GetComponentInChildren<CharacterController>();
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _animator = _characterModel.GetComponent<Animator>();
            _playerAim = _characterModel.GetComponent<PlayerAim>();
            _weapon = GetComponentInChildren<Weapon>();
            _gravity = _adjustGravity;
        }
        private void FixedUpdate()
        {
            if (_inBallForm)
            {
                _controller.enabled = false;
                BallMovement();
            }
            if (_z != 0)
                _z = 0;
        }
        private void Update()
        {
            if (GameManager.Instance.IsPaused || GameManager.Instance.IsUpgrading) return;
            _deltaTime = Time.deltaTime;
            _ballModeTriggered = _inputManager.controls.Standard.BallMode.triggered;
            _jumpTriggered = _inputManager.controls.Standard.Jump.triggered;
            if (_transform.position.z != 0)
                _transform.position = new Vector3(_transform.position.x, _transform.position.y, _z);
            if (!_inBallForm && !_isCrouching && _controller.enabled)
             Movement();
            
            //else if (_inBallForm)
            //{
            //    _controller.enabled = false;
            //    BallMovement();
            //}
            
            if(_controller.enabled || _inBallForm)
            {
                FaceDirection();
                if (_ballModeTriggered && !_inBallForm && _controller.isGrounded && _upgrades.ballMode)
                {
                    _mainCamera.trackedObject = _ballForm;
                   _ballFormTransform.position = _transform.position;
                    _characterModel.SetActive(false);
                    _ballForm.SetActive(true);
                    _inBallForm = true;
                    _rigidbody = GetComponentInChildren<Rigidbody>();
                    _rigidbody.velocity = Vector3.zero;
                }
                else if (_ballModeTriggered && _inBallForm)
                {
                    _mainCamera.trackedObject = gameObject;
                    _transform.position = new Vector3(_ballFormTransform.position.x, _ballFormTransform.position.y, _z);
                    _controller.enabled = true;
                    _characterModel.SetActive(true);
                    _ballForm.SetActive(false);
                    _inBallForm = false;
                    _ballFormTransform.position = new Vector3(_transform.position.x, _transform.position.y, _z);
                    _rigidbody.velocity = Vector3.zero;
                }
            }
             _animator.SetFloat(_horizontalAP, _horizontal != 0 ? 1 : 0);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_controller.isGrounded && !_isWallJumping && !_grabbingLedge && _wallJumpCount < _wallJumpLimit)
            {
                if (hit.transform.CompareTag("Wall"))
                {
                    _wallSurfaceNormal = hit.normal;
                    _canWallJump = true;
                    _canDoubleJump = false;
                    _playerAim.AimWeight = 0;
                }
            }
            else
            {
                _canWallJump = false;
                _isWallJumping = false;
                _playerAim.AimWeight = 1;
                _animator.SetFloat(_wallJumpingAP, 0);
            }
        }

        private void BallMovement()
        {
            _direction = new Vector3(_horizontal, 0, _z);
            _velocity = _direction * _speed * 1.5f;
            _rigidbody.AddForce(_velocity, ForceMode.Force);
            _velocity.x = _velocity.x > _ballModeXVelocityLimitRight ? _ballModeXVelocityLimitRight : _velocity.x;
            _velocity.x = _velocity.x < _ballModeXVelocityLimitLeft ? _ballModeXVelocityLimitLeft : _velocity.x;
        }

        private void FaceDirection()
        {
            if (_horizontal < 0)
                _characterModelTransform.localRotation = Quaternion.Euler(0, -90, 0);

            else if (_horizontal > 0)
                _characterModelTransform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        private void Movement()
        {
            _direction = new Vector3(_horizontal, 0, _z);
            _velocity = _direction * _speed;
            _characterModelTransform.localPosition = _modelPosition;

            if (_controller.isGrounded)
            {
                _animator.SetFloat(_groundedAP, 1);
                _animator.SetBool(_droppingAP, false);
                _animator.SetFloat(_jumpingAP, 0);
                _wallJumpCount = 0;
                if (_jumpTriggered)
                 Jump();
            }

            else
            {
                _animator.SetFloat(_groundedAP, 0);
                if (_jumpTriggered && !_isWallJumping)
                {
                    if (_canDoubleJump && _upgrades.doubleJump || _canWallJump)
                    {
                        if (_canWallJump && !_isWallJumping)
                             WallJump();
                        
                        DoubleJump();
                    }
                    _animator.SetFloat(_jumpingAP, 1);
                }
                if (_isHanging)
                {
                    _animator.SetFloat(_jumpingAP, 0);
                    _animator.SetFloat(_hangingAP, 1);
                    _gravity = 0;
                    _canDoubleJump = false;
                    _canWallJump = false;
                }
                if (_vertical < 0.5 && _isHanging)
                {
                    _animator.SetFloat(_hangingAP, 0);
                    _animator.SetBool(_droppingAP, true);
                    _isHanging = false;
                    _gravity = _adjustGravity;
                }
                _yVelocity -= _gravity;
            }
            _velocity.y = _yVelocity < _yVelocityDownLimit ? _yVelocityDownLimit : _yVelocity;
            _velocity.y = _yVelocity > _yVelocityUpLimit ? _yVelocityUpLimit : _yVelocity;
            //Debug.Log($"Y Velocity = {_velocity.y}.");

            //if (!_controller.isGrounded && !_isHanging && !_isWallJumping && !_canDoubleJump && !_jumpTriggered && !_grabbingLedge && _controller.enabled)
            //{

            //    //_animator.SetFloat(_fallingAP, 1.0f);
            //}
            //else
            // _animator.SetFloat(_fallingAP, 0f);
            _controller.Move(_velocity * _deltaTime);
        }

        private void Jump()
        {
            _yVelocity = _jumpHeight + Mathf.Abs(_velocity.x);
            _canDoubleJump = true;
            _animator.SetFloat(_jumpingAP, 1);
            _animator.SetFloat(_groundedAP, 0);
            Debug.Log($"Jump initiated at {_yVelocity}");
        }
        private void DoubleJump()
        {
            _yVelocity = _jumpHeight + Mathf.Abs(_velocity.x);
            _canDoubleJump = false;
            Debug.Log($"Double Jump initiated at {_yVelocity}");
        }

        private void WallJump()
        {
            _wallJumpCount++;
            _isWallJumping = true;
            _animator.SetFloat(_jumpingAP, 0);
            _animator.SetFloat(_wallJumpingAP, 1);
            _velocity = _wallSurfaceNormal * (_speed * _wallJumpBounceOffModifier);
            _canDoubleJump = false;
            _canWallJump = false;
            Debug.Log($"Wall Jump initiated at {_yVelocity}");
        }

        private void OnCrouchInput(float c)
        {
            if (c == 1 && _controller.isGrounded)
            {
                _animator.SetFloat(_crouchAP, c);
                _animator.SetFloat(_horizontalAP, 0);
                _isCrouching = true;
                _aimTransform.localPosition = new Vector3(_aimTransform.localPosition.x, _aimTargetCrouchingPos, _aimTransform.localPosition.z);
            }
            else if (c == 1 && !_controller.isGrounded)
                return;

            else
            {
                _animator.SetFloat(_crouchAP, c);
                _isCrouching = false;
                _aimTransform.localPosition = new Vector3(_aimTransform.localPosition.x, _aimTargetStandingPos, _aimTransform.localPosition.z);
            }
        }

        private void OnMoveInput(float x, float y)
        {
            _horizontal = x;
            _vertical = y;
        }
        /// <summary>
        /// Triggers animation for climbing up on a ledge and
        /// sets required states.
        /// </summary>
        public void ClimbUp()
        {
            _animator.SetFloat(_grabLedgeAP, 0f);
            _animator.SetFloat(_hangingAP, 0f);
            _animator.SetFloat(_jumpingAP, 0f);
            _playerAim.AimWeight = 1;
            _isHanging = false;
            _grabbingLedge = false;
            _jumpTriggered = false;
            _transform.localPosition = _activeLedge.GetStandPosition().localPosition;
            _controller.enabled = true;
            _activeLedge = null;
            _transform.parent = null;
        }
        /// <summary>
        /// Logic for adjusting character to seamlessly grab a ledge.
        /// </summary>
        /// <param name="handPos"></param>
        /// <param name="currentLedge"></param>
        /// <param name="freeHang"></param>
        public void GrabLedge(Vector3 handPos, Ledge currentLedge, bool freeHang)
        {
            if (!_inBallForm)
            {
                _controller.enabled = false;
                _animator.SetFloat(_grabLedgeAP, 1.0f);
              
                if (!freeHang)
                {
                    _animator.SetFloat(_hangingAP, 1.0f);
                    _animator.SetTrigger(_bracedAP);
                }
                else
                    _animator.SetFloat(_hangingAP, 1.0f);

                _playerAim.AimWeight = 0;
                _velocity = Vector3.zero;
                _isHanging = true;
                _grabbingLedge = true;
                _activeLedge = currentLedge;
                _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, handPos, 5f);
            }
        }

        /// <summary>
        /// Pass in the ID and name of the upgrade to activate.
        /// </summary>
        /// <param name="upgradeID"></param>
        /// <param name="upgradeName"></param>
        public void ActivateUpgrade(int upgradeID, string upgradeName)
        {
            switch(upgradeID)
            {
                case 0:
                    _upgrades.ballBombs = true;
                    break;
                case 1:
                    _upgrades.ballMode = true;
                    break;
                case 2:
                    _upgrades.chargeBeam = true;
                    break;
                case 3:
                    _upgrades.doubleJump = true;
                    break;
                case 4:
                    _upgrades.missiles = true;
                    _weapon.SecondaryAmmo += 5;
                    UIManager.Instance.MissileText.gameObject.SetActive(true);
                    UIManager.Instance.MissilesTextUpdate(5);
                    break;
                default:
                    Debug.Log("No upgrade specified.");
                    break;
            }
            StartCoroutine(UIManager.Instance.AcquireUpgradeRoutine(upgradeName));
        }
    }
}