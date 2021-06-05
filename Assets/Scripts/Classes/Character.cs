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
        private float _yVelocity;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();

        }
        private void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _direction = new Vector3(_horizontal, 0, 0);
            _velocity = _direction * _speed;
           
            if (_controller.isGrounded)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    _yVelocity = _jumpHeight;
                }
            }
            else
                _yVelocity -= _gravity;

            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}