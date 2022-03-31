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
       
        private void Update() => RotateThis(_rotationVector);

        private void RotateThis(Vector3 rotationVector) => transform.Rotate(rotationVector * _speed);
    }
}
