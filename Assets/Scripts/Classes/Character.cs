// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour
    {
        private CharacterController _controller;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private Transform _animatorRoot;
        private PlayerAim _playerAim;
        private float _horizontal;
        private float _vertical;
        //private float _runSpeed = 10.0f;
        //private float _defaultSpeed;
        private float _yVelocity;
        private float _aimTargetStandingPos = 1.4f;
        private float _aimTargetCrouchingPos = 0.85f;
        private bool _canDoubleJump;
        private bool _canWallJump;
        private bool _jumpTriggered;
        private bool _ballModeTriggered;
        private bool _isWallJumping;
        private bool _inBallForm;
        private bool _isCrouching;
        private bool _hanging;
        private bool _grabbingLedge;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        //Animator Parameters
        private readonly int _groundedAP = Animator.StringToHash("grounded");
        private readonly int _horizontalAP = Animator.StringToHash("horizontal");
        private readonly int _jumpingAP = Animator.StringToHash("jumping");
        private readonly int _droppingAP = Animator.StringToHash("dropping");
        private readonly int _wallJumpingAP = Animator.StringToHash("wallJumping");
        private readonly int _hangingAP = Animator.StringToHash("hanging");
        private readonly int _grabLedgeAP = Animator.StringToHash("grabLedge");
        private readonly int _crouchAP = Animator.StringToHash("crouch");
        [SerializeField] private int _collectibles;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _gravity;
        [SerializeField] private float _adjustGravity;
        [SerializeField] private float _jumpHeight = 15.0f;
        [SerializeField] private float _collectibleDetectionRadius;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private GameObject _ballForm;
        [SerializeField] private GameObject _aimTarget;
        [SerializeField] private Vector3 _modelPosition;
        [SerializeField] private LayerMask _detectSurfaceLayers;
        [SerializeField] private LayerMask _collectibleLayerMask;
        [SerializeField] private InputManagerSO _inputManager;
        public InputManagerSO InputManager { get; set; }
        //[SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;
        [SerializeField] private CameraController _mainCamera;

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

        private void OnMoveInput(float x, float y)
        {
            _horizontal = x;
            _vertical = y;
        }
        private void OnCrouchInput(float c)
        {
            if (c == 1 && _controller.isGrounded)
            {
                _animator.SetFloat(_crouchAP, c);
                _animator.SetFloat(_horizontalAP, 0);
                _isCrouching = true;
                _aimTarget.transform.localPosition = new Vector3(_aimTarget.transform.localPosition.x, _aimTargetCrouchingPos, _aimTarget.transform.localPosition.z);
            }
            else if(c == 1 && !_controller.isGrounded)
             return;
            
            else
            {
                _animator.SetFloat(_crouchAP, c);
                _isCrouching = false;
               _aimTarget.transform.localPosition = new Vector3(_aimTarget.transform.localPosition.x, _aimTargetStandingPos, _aimTarget.transform.localPosition.z);
            }
        }

        private void Start()
        {
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
        private void Update()
        {
            _ballModeTriggered = _inputManager.controls.Standard.BallMode.triggered;
            _jumpTriggered = _inputManager.controls.Standard.Jump.triggered;
        }
        private void FixedUpdate()
        {
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
                    _ballForm.transform.position = transform.position;
                    _characterModel.SetActive(false);
                    _ballForm.SetActive(true);
                    _inBallForm = true;
                    _rigidbody = GetComponentInChildren<Rigidbody>();
                    _rigidbody.velocity = Vector3.zero;
                }
                else if (_ballModeTriggered && _inBallForm)
                {
                    _mainCamera.trackedObject = this.gameObject;
                    transform.position = _ballForm.transform.position;
                    _controller.enabled = true;
                    _characterModel.SetActive(true);
                    _ballForm.SetActive(false);
                    _inBallForm = false;
                    _ballForm.transform.position = transform.position;
                    _rigidbody.velocity = Vector3.zero;
                }
            }
            DetectSurface();
            DetectCollectible();
            if (_horizontal != 0)
                _animator.SetFloat(_horizontalAP, 1);
            else
                _animator.SetFloat(_horizontalAP, 0);
          
        }
        private void Movement()
        {
            _direction = new Vector3(_horizontal, 0, 0);
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
                if (_hanging)
                {
                    _animator.SetFloat(_jumpingAP, 0);
                    _animator.SetFloat(_hangingAP, 1);
                    _gravity = 0;
                    _canDoubleJump = false;
                    _canWallJump = false;
                }
                if (_vertical < 0.5 && _hanging)
                {
                    _animator.SetFloat(_hangingAP, 0);
                    _animator.SetBool(_droppingAP, true);
                    _hanging = false;
                    _gravity = _adjustGravity;
                }
                _yVelocity -= _gravity;
            }
            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }
        private void LedgeMovement()
        {
            if (_vertical < 0)
            {
                _animator.SetFloat(_grabLedgeAP, 0f);
                _grabbingLedge = false;
                _animatorRoot = null;
                _characterModel.transform.localPosition = _modelPosition;
                _controller.enabled = true;
                _yVelocity -= _gravity;
            }
        }
        private void BallMovement()
        {
            _direction = new Vector3(_horizontal, 0, 0);
            _velocity = _direction * _speed *1.5f;
            _rigidbody.AddForce(_velocity, ForceMode.Force);
        }
        private void FaceDirection()
        {
            if (_horizontal < 0)
                _characterModel.transform.localRotation = Quaternion.Euler(0, -90, 0);

            else if (_horizontal > 0)
                _characterModel.transform.localRotation = Quaternion.Euler(0, 90, 0);
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
        private void DetectSurface()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, Vector3.up, out hitInfo, 2.0f, _detectSurfaceLayers))
            {
                var hangable = hitInfo.collider.GetComponent<IHang>();
                if (hangable != null && _vertical > 0)
                    _hanging = true;
            }
            
            else
            {
                _animator.SetFloat(_hangingAP, 0);
                _hanging = false;
                _gravity = _adjustGravity;
            }
        }
        
        private void DetectCollectible()
        {
            int maxColliders = 5;
            Collider[] results = new Collider[maxColliders];
            int numberColliders = Physics.OverlapSphereNonAlloc(transform.position,
                                                                _collectibleDetectionRadius,
                                                                results,
                                                                _collectibleLayerMask);

            for (int i = 0; i < numberColliders; i++)
            {
                results[i].transform.position = Vector3.Lerp(results[i].transform.position, transform.position, 3f * Time.deltaTime);

            }
        }
        private void AnimLerp()
        {
            if (!_animatorRoot) return;

            if (Vector3.Distance(this.transform.position, _animatorRoot.position) > 0.1f)
            {
                float lerpSpeed = 60.0f;
                _characterModel.transform.rotation = Quaternion.Lerp(_characterModel.transform.rotation,
                                                     _animatorRoot.rotation,
                                                     Time.deltaTime * lerpSpeed);
                _characterModel.transform.position = Vector3.Lerp(_characterModel.transform.position,
                                                  _animatorRoot.position,
                                                  Time.deltaTime * lerpSpeed);
            }
            else
            {
                _characterModel.transform.position = _animatorRoot.position;
                _characterModel.transform.rotation = _animatorRoot.rotation;
            }
        }
    }
}