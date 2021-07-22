// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PlatformParenter : MonoBehaviour
    {
        [SerializeField] private GameObject _childObject;

        private void Update()
        {
            if(_childObject != null)
            {
                var ballForm = _childObject.GetComponent<Character>().InBallForm;
                if (ballForm)
                {
                    //Debug.Log("Player entered Ball Form!");
                    _childObject.transform.parent = null;
                    _childObject = null;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character>())
            {
                var ballForm = other.GetComponent<Character>().InBallForm;
                if (!ballForm)
                {
                    //Debug.Log("Found Character!");
                    other.transform.parent = this.transform;
                    _childObject = other.gameObject;
                }
                else
                    return;
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