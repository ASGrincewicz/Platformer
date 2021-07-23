// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;

namespace Veganimus.Platformer
{
    public class Rotate : MonoBehaviour
    {
        public Rotate() { }

        public Rotate(Vector3 vector3) => _rotationVector = vector3;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _rotationVector;
        private float _deltaTime;

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            RotateThis(_rotationVector);
        }

        public void RotateThis(Vector3 rotationVector) => transform.Rotate(rotationVector * _speed * _deltaTime);
    }


}
