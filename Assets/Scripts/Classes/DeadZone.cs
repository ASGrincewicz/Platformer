using UnityEngine;

namespace Veganimus.Platformer
{
    public class DeadZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            var damageable = other.collider.GetComponent<IDamageable>();
            if(other.collider.tag == "Player")
            {
                Debug.Log("Damaging Player...");
                damageable.Damage(9999);
            }
        }
    }
}


