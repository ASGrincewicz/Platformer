// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Ledge : MonoBehaviour, ILedge
    {
        [SerializeField] private bool _isFreeHang;
        [SerializeField] private Vector3 _handPos;
        [SerializeField] private Transform _standPos;
        private const string _grabTag = "LedgeGrab";
        private Character _player;

        private void Start()
        {
            _player = Character.Instance;
            if (_player == null)
                Debug.LogError("Character is NULL!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == _grabTag)
            {
                if (_player != null)
                {
                    _player.transform.parent = this.transform;
                    _player.GrabLedge(_handPos, this, _isFreeHang);
                }
                else
                    return;
            }
        }

        public Transform GetStandPosition() => _standPos;
    }
}