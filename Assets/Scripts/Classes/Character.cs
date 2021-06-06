// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour, ICollector
    {
        private CharacterController _controller;
        private float _horizontal;
        private float _vertical;
        [SerializeField] private float _speed = 5f;
        private float _runSpeed = 10.0f;
        private float _defaultSpeed;
        [SerializeField] private float _gravity = 1.0f;
        [SerializeField] private float _jumpHeight = 15.0f;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        private float _yVelocity;
        private bool _canDoubleJump;
        private bool _canWallJump;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private bool _hanging;
        [SerializeField] private LayerMask _detectSurfaceLayers;
        private ICollector _collector;

        public bool IsCollecting { get; set; }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _collector = GetComponent<ICollector>();
            _defaultSpeed = _speed;
        }
        private void Update()
        {
            Movement();
            Run();
            FaceDirection();
            DetectSurface();
            IsCollecting = Collecting();
        }
        private void Movement()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _direction = new Vector3(_horizontal, 0, 0);
            _velocity = _direction * _speed;

            if (_controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = true;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if (_canDoubleJump || _canWallJump)
                    {
                        if (_canWallJump)
                        {
                            _velocity = _wallSurfaceNormal * (_speed * 3);
                            _canDoubleJump = false;
                        }
                        _yVelocity = _jumpHeight;
                        _canDoubleJump = false;
                    }
                    //if (_hanging)
                    //{
                    //    _gravity = 0;
                    //    _canDoubleJump = false;
                    //    _canWallJump = false;
                    //}
                }
                if (_hanging)
                {
                    _gravity = 0;
                    _canDoubleJump = false;
                    _canWallJump = false;
                }
                if (_vertical < 0 && _hanging)
                {
                    _hanging = false;
                    _gravity = 1;
                }
                _yVelocity -= _gravity;
            }

            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }
        private void Run()
        {
            if (Input.GetKey(KeyCode.LeftShift) && _controller.isGrounded)
                _speed = _runSpeed;
            else
                _speed = _defaultSpeed;
        }
        private void FaceDirection()
        {

            if (_horizontal < 0)
                _characterModel.transform.localRotation = new Quaternion(0, -180, 0,0);
            else if (_horizontal > 0)
                _characterModel.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_controller.isGrounded)
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
                _canWallJump = false;
        }
        private void DetectSurface()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(_characterModel.transform.position, Vector3.up, out hitInfo, 2.5f, _detectSurfaceLayers))
            {
                var hangable = hitInfo.collider.GetComponent<IHang>();
                if (hangable != null)
                    _hanging = true;
            }
            else
            {
                _hanging = false;
                _gravity = 1.0f;
            }
        }
        private bool Collecting()
        {
            if (Input.GetKey(KeyCode.C))
                return true;
            else
                return false;
        }
    }
}