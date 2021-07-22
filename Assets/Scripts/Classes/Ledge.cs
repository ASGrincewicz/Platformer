// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Ledge : MonoBehaviour, ILedge
    {
        [SerializeField] private Vector3 _handPos;
        private const string _grabTag = "LedgeGrab";
        private Character _player;

        private void Start()
        {
            _player = GameObject.Find("Character").GetComponent<Character>();
            if (_player == null)
                Debug.LogError("Character is NULL!");
            else if (_player != null)
                Debug.Log("Found Player.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == _grabTag)
            {
                if (_player != null)
                {
                    _player.transform.parent = this.transform;
                    _player.GrabLedge(_handPos, this);
                }
                else
                    return;
            }
        }




    }
}