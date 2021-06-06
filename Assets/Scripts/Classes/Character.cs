// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Character : MonoBehaviour
    {
        private CharacterController _controller;
        private float _horizontal;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _gravity = 1.0f;
        [SerializeField] private float _jumpHeight = 15.0f;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Vector3 _wallSurfaceNormal;
        private float _yVelocity;
        private bool _canDoubleJump;
        private bool _canWallJump;
        [SerializeField] private GameObject _characterModel;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }
        private void Update()
        {
            Movement();
            FaceDirection();
        }
        private void Movement()
        {
            _horizontal = Input.GetAxis("Horizontal");
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
                        }
                        _yVelocity = _jumpHeight;
                        _canDoubleJump = false;
                    }
                }
                _yVelocity -= _gravity;
            }

            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
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
                }
            }
            else
                _canWallJump = false;
        }
    }
}