// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;

namespace Veganimus.Platformer
{
    class Upgrade : Collectible
    {
        [Tooltip("0 = BallBombs, 1 = BallMode, 2 = ChargeBeam, 3 = DoubleJump, 4 = Missiles")]
        [SerializeField] private int _upgradeID;
        [SerializeField] private string _upgradeName;

        protected override void Start() => _transform = transform;

        protected override void Update() { }

        protected override void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") && _collectibleType == CollectibleType.Upgrade)
            {
                var player = Character.Instance;

                if (player != null)
                {
                    if (!player.Upgrades.ballMode && _upgradeID == 0)
                    {
                        _collected = false;
                        return;
                    }
                    else
                    {
                        _collected = true;
                        player.ActivateUpgrade(_upgradeID, _upgradeName);
                        GameManager.Instance.UpgradesCollected++;
                    }
                }
                else
                    Debug.Log("Player not found.");
            }
            if (_collected)
                Destroy(gameObject);
        }
    }
}
