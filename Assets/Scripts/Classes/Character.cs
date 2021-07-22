// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private byte _collectibles;
        [SerializeField] private float _adjustGravity;
        [SerializeField] private float _collectibleDetectionRadius;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight = 15.0f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private LayerMask _collectibleLayerMask;
        [SerializeField] private LayerMask _detectSurfaceLayers;
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
        private readonly int _crouchAP = Animator.StringToHash("crouch");
        private readonly int _droppingAP = Animator.StringToHash("dropping");
        private readonly int _grabLedgeAP = Animator.StringToHash("grabLedge");
        private readonly int _groundedAP = Animator.StringToHash("grounded");
        private readonly int _hangingAP = Animator.StringToHash("hanging");
        private readonly int _horizontalAP = Animator.StringToHash("horizontal");
        private readonly int _jumpingAP = Animator.StringToHash("jumping");
        private readonly int _wallJumpingAP = Animator.StringToHash("wallJumping");
        private float _aimTargetCrouchingPos = 0.85f;
        private float _aimTargetStandingPos = 1.4f;
        private float _deltaTime;
        private float _horizontal;
        private float _vertical;
        private const float _z = 0;
        private float _yVelocity;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        private Animator _animator;
        private CharacterController _controller;
        private Collider[] _collectiblesDetected = new Collider[5];
        private PlayerAim _playerAim;
        private Rigidbody _rigidbody;
        private Transform _aimTransform;
        private Transform _animatorRoot;
        private Transform _transform;
        private Transform _characterModelTransform;
        private Transform _ballFormTransform;
        public bool InBallForm { get { return _inBallForm; } }
        public InputManagerSO InputManager { get; set; }

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
            _gravity = _adjustGravity;
        }
        //private void FixedUpdate()
        //{
        //    AnimLerp();
        //}
        private void FixedUpdate()
        {
            DetectSurface();
            DetectCollectible();
        }
        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _ballModeTriggered = _inputManager.controls.Standard.BallMode.triggered;
            _jumpTriggered = _inputManager.controls.Standard.Jump.triggered;
            if (_transform.position.z != 0)
                _transform.position = new Vector3(_transform.position.x, _transform.position.y, _z);
            if (!_inBallForm && !_isCrouching && _controller.enabled)
             Movement();
            
            else if (_inBallForm)
            {
                _controller.enabled = false;
                BallMovement();
            }
            else if (!_controller.enabled && _grabbingLedge)
             LedgeMovement();
            
            if(_controller.enabled || _inBallForm)
            {
                FaceDirection();
                if (_ballModeTriggered && !_inBallForm && _controller.isGrounded)
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
                    _mainCamera.trackedObject = this.gameObject;
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
            if (!_controller.isGrounded && !_isWallJumping && !_grabbingLedge)
            {
                var wall = hit.collider.GetComponent<IWall>();

                if (wall != null)
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
        //private void AnimLerp()
        //{
        //    if (!_animatorRoot) return;

        //    if (Vector3.Distance(_transform.position, _animatorRoot.position) > 0.1f)
        //    {
        //        float lerpSpeed = 60.0f;
        //        _characterModelTransform.rotation = Quaternion.Lerp(_characterModelTransform.rotation,
        //                                             _animatorRoot.rotation,
        //                                             _deltaTime * lerpSpeed);
        //        _characterModelTransform.position = Vector3.Lerp(_characterModelTransform.position,
        //                                          _animatorRoot.position,
        //                                          _deltaTime * lerpSpeed);
        //    }
        //    else
        //    {
        //        _characterModelTransform.position = _animatorRoot.position;
        //        _characterModelTransform.rotation = _animatorRoot.rotation;
        //    }
        //}

        private void BallMovement()
        {
            _direction = new Vector3(_horizontal, 0, _z);
            _velocity = _direction * _speed * 1.5f;
            _rigidbody.AddForce(_velocity, ForceMode.Force);
        }

        private void DetectCollectible()
        {
            byte numberColliders = (byte)Physics.OverlapSphereNonAlloc(_transform.localPosition,
                                                                _collectibleDetectionRadius,
                                                                _collectiblesDetected,
                                                                _collectibleLayerMask);

           for (byte i = 0; i < numberColliders; i++)
           {
            _collectiblesDetected[i].transform.localPosition = Vector3.MoveTowards(_collectiblesDetected[i].transform.localPosition, _transform.localPosition, 3f * _deltaTime);

           }
            Array.Clear(_collectiblesDetected, 0, _collectiblesDetected.Length);
        }

        private void DetectSurface()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(_transform.localPosition, Vector3.up, out hitInfo, 2.0f, _detectSurfaceLayers))
            {
                var hangable = hitInfo.collider.GetComponent<IHang>();
                if (hangable != null && _vertical > 0)
                    _isHanging = true;
            }

            else
            {
                _animator.SetFloat(_hangingAP, 0);
                _isHanging = false;
                _gravity = _adjustGravity;
            }
        }


        private void FaceDirection()
        {
            if (_horizontal < 0)
                _characterModelTransform.localRotation = Quaternion.Euler(0, -90, 0);

            else if (_horizontal > 0)
                _characterModelTransform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        private void LedgeMovement()
        {
            if (_vertical < 0)
            {
                _animator.SetFloat(_grabLedgeAP, 0f);
                _grabbingLedge = false;
                _animatorRoot = null;
                _characterModelTransform.localPosition = _modelPosition;
                _controller.enabled = true;
                _yVelocity -= _gravity;
            }
        }

        private void Movement()
        {
            _direction = new Vector3(_horizontal, 0, _z);
            _velocity = _direction * _speed;

            if (_controller.isGrounded)
            {
                _animator.SetFloat(_groundedAP, 1);
                _animator.SetBool(_droppingAP, false);
                _animator.SetFloat(_jumpingAP, 0);
                if (_jumpTriggered)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = true;
                    _animator.SetFloat(_jumpingAP, 1);
                    _animator.SetFloat(_groundedAP, 0);
                }
            }
            else
            {
                _animator.SetFloat(_groundedAP, 0);
                if (_jumpTriggered && !_isWallJumping)
                {
                    if (_canDoubleJump || _canWallJump)
                    {
                        if (_canWallJump && !_isWallJumping)
                        {
                            _isWallJumping = true;
                            _animator.SetFloat(_jumpingAP, 0);
                            _animator.SetFloat(_wallJumpingAP, 1);
                            _velocity = _wallSurfaceNormal * (_speed * 5);
                            _canDoubleJump = false;
                            _canWallJump = false;
                        }
                        _yVelocity = _jumpHeight;
                        _canDoubleJump = false;
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
            _velocity.y = _yVelocity;
            _controller.Move(_velocity * _deltaTime);
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

        public void GrabLedge(Transform anchorPos)
        {
            if (!_inBallForm)
            {
                _animatorRoot = anchorPos;
                _animator.SetFloat(_grabLedgeAP, 1.0f);
                _grabbingLedge = true;
                _controller.enabled = false;
            }
        }
    }
}