// Aaron Grincewicz ASGrincewicz@icloud.com 7/25/2021
using System;
using UnityEngine;

namespace Veganimus.Platformer
{
    public class DetectCollectible : MonoBehaviour
    {
        [SerializeField, Tooltip("The area around the player in which collectibles are detected.")]
        private float _collectibleDetectionRadius;
        [SerializeField,Tooltip("Layer on which collectible objects exist.")] private LayerMask _collectibleLayerMask;
        private readonly Collider[] _collectiblesDetected = new Collider[5];
        private Transform _transform;

        private void Start() => _transform = transform;

        private void FixedUpdate() => DetectCollectibles();

        private void DetectCollectibles()
        {
            int numberColliders = Physics.OverlapSphereNonAlloc(_transform.localPosition,
                                                                _collectibleDetectionRadius,
                                                                _collectiblesDetected,
                                                                _collectibleLayerMask);
            if (numberColliders != 0)
            {
                for (var i = 0; i < numberColliders; i++)
                {
                    if (_collectiblesDetected[i].GetComponent<Collectible>().CanAbsorb)
                        _collectiblesDetected[i].transform.localPosition = Vector3.MoveTowards(_collectiblesDetected[i].transform.localPosition, _transform.localPosition, 3f * Time.deltaTime);
                    else
                        return;
                }
                Array.Clear(_collectiblesDetected, 0, _collectiblesDetected.Length);
            }
        }
    }
}
