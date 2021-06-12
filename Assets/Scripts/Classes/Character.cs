// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour
    {
        private CharacterController _controller;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private float _horizontal;
        private float _vertical;
        private float _runSpeed = 10.0f;
        private float _defaultSpeed;
        private float _yVelocity;
        private bool _canDoubleJump;
        private bool _canWallJump;
        private bool _jumpTriggered;
        private bool _isWallJumping;
        private bool _inBallForm;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _gravity = 1.0f;
        [SerializeField] private float _jumpHeight = 15.0f;
        [SerializeField] private float _collectibleDetectionRadius;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private GameObject _ballForm;
        [SerializeField] private bool _hanging;
        [SerializeField] private LayerMask _detectSurfaceLayers;
        [SerializeField] private LayerMask _collectibleLayerMask;
        [SerializeField] private InputManager _inputManager;
        public GameObject testIcon;

        private void OnEnable()
        {
            _inputManager.moveAction += OnMoveInput;
            
        }
        private void OnDisable()
        {
            _inputManager.moveAction -= OnMoveInput;
        }

        private void OnMoveInput(float x, float y)
        {
            _horizontal = x;
            _vertical = y;
        }

        private void Start()
        {
            _controller = GetComponentInChildren<CharacterController>();
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _inputManager = GetComponent<InputManager>();
            _animator = _characterModel.GetComponent<Animator>();
            _defaultSpeed = _speed;
           
        }
        private void Update()
        {
            var ballModeTriggered = _inputManager.controls.Standard.BallMode.triggered;
            if (!_inBallForm)
            {
                Movement();
                FaceDirection();
            }
            else if(_inBallForm)
            {
                BallMovement();
            }
            DetectSurface();
            DetectCollectible();
            if (_horizontal != 0)
                _animator.SetFloat("horizontal", 1);
            else
                _animator.SetFloat("horizontal", 0);
            if(ballModeTriggered && !_inBallForm)
            {
                _characterModel.SetActive(false);
                _ballForm.SetActive(true);
                _inBallForm = true;
                _rigidbody = GetComponentInChildren<Rigidbody>();
                _controller.height = 0.5f;
                _controller.center = new Vector3(0, -0.46f, 0);
            }
            else if(ballModeTriggered && _inBallForm)
            {
                _characterModel.SetActive(true);
                _ballForm.SetActive(false);
                _inBallForm = false;
                _controller.height = 1.92f;
                _controller.center = new Vector3(0, 0.2f, 0);
            }
        }
        private void Movement()
        {
            _jumpTriggered = _inputManager.controls.Standard.Jump.triggered;
            _direction = new Vector3(_horizontal, 0, 0);
            _velocity = _direction * _speed;
          
            if (_controller.isGrounded)
            {
                _animator.SetFloat("grounded", 1);
                _animator.SetBool("dropping", false);
                _animator.SetFloat("jumping", 0);
                if (_jumpTriggered)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = true;
                    _animator.SetFloat("jumping", 1);
                    _animator.SetFloat("grounded", 0);
                }
            }
            else
            {
                _animator.SetFloat("grounded", 0);
                if (_jumpTriggered && !_isWallJumping)
                {
                    if (_canDoubleJump || _canWallJump)
                    {
                        if (_canWallJump && !_isWallJumping)
                        {
                            _isWallJumping = true;
                            _animator.SetFloat("jumping", 0);
                            _animator.SetFloat("wallJumping", 1);
                            _velocity = _wallSurfaceNormal * (_speed * 10);
                            _canDoubleJump = false;
                            _canWallJump = false;
                        }
                        _yVelocity = _jumpHeight;
                        _canDoubleJump = false;
                    }
                    _animator.SetFloat("jumping", 1);
                }
                if (_hanging)
                {
                    _animator.SetFloat("jumping", 0);
                    _animator.SetFloat("hanging", 1);
                    _gravity = 0;
                    _canDoubleJump = false;
                    _canWallJump = false;
                }
                if (_vertical < 0.5 && _hanging)
                {
                    _animator.SetFloat("hanging", 0);
                    _animator.SetBool("dropping", true);
                    _hanging = false;
                    _gravity = 1;
                }
                _yVelocity -= _gravity;
            }
            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }
        private void BallMovement()
        {
            _jumpTriggered = _inputManager.controls.Standard.Jump.triggered;
            _direction = new Vector3(_horizontal, 0, 0);
            _velocity = _direction * _speed;
            if(_controller.isGrounded)
            {
                if (_jumpTriggered)
                 _yVelocity = _jumpHeight;
            }
            else
             _yVelocity -= _gravity;
            
            _velocity.y = _yVelocity;
            //_rigidbody.MoveRotation(_velocity);
            _controller.Move(_velocity * Time.deltaTime);
            
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
            if (!_controller.isGrounded && !_isWallJumping)
            {
                var wall = hit.collider.GetComponent<IWall>();
                if (wall != null)
                {
                    _wallSurfaceNormal = hit.normal;
                    _canWallJump = true;
                    _canDoubleJump = false;
                }
            }
            else
            {
                _canWallJump = false;
                _isWallJumping = false;
                _animator.SetFloat("wallJumping", 0);
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
                _animator.SetFloat("hanging", 0);
                _hanging = false;
                _gravity = 1.0f;
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

                if (Vector3.Distance(transform.position, results[i].transform.position) < 0.5f)
                {
                    Destroy(results[i].gameObject);
                    
                }

            }
        }
    }
}