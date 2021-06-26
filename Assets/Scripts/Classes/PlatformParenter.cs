// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PlatformParenter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                Debug.Log("Found Character!");
                other.transform.parent = this.transform;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                other.transform.parent = null;
            }
        }
    }
}