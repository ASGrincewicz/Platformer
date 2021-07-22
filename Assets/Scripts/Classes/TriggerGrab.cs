// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class TriggerGrab : MonoBehaviour
    {
        [SerializeField] private GameObject _anchorPos;

        private void OnTriggerEnter(Collider other)
        {
            //if(other.tag == "Player")
            //other.GetComponent<Character>().GrabLedge(_anchorPos.transform);
        }
    }
}