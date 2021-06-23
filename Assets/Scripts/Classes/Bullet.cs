// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10.0f;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            Destroy(gameObject, 3.0f);
        }
    }
}