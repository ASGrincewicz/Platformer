// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private GameObject _aimTarget;

        [SerializeField] private float _aimWeight = 1f;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator != null)
                Debug.Log("found animator!");
        }

        private void Update()
        {
            
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetLookAtPosition(_aimTarget.transform.position);
            _animator.SetLookAtWeight(_aimWeight);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _aimTarget.transform.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _aimWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }
    }
}