// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;

namespace Veganimus.Platformer
{
    class Upgrade : Collectible
    {
        [Tooltip("0 = BallBombs, 1 = BallMode, 2 = ChargeBeam, 3 = DoubleJump, 4 = Missiles")]
        [SerializeField] private int _upgradeID;

        protected override void Start() => _transform = transform;

        protected override void Update() { }

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "Player" && _collectibleType == CollectibleType.Upgrade)
            {
                _collected = true;
                //Enable upgrade on player
                var player = other.gameObject.GetComponentInParent<Character>();
                if (player != null)
                    player.ActivateUpgrade(_upgradeID);
                else
                    Debug.Log("Player not found.");
            }
            if (_collected)
                Destroy(this.gameObject);
        }
    }
}
