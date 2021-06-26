// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private GameObject _aimTarget;
        [SerializeField] private float _aimWeight = 1f;
        public float AimWeight { get { return _aimWeight; } set { _aimWeight = value; } }
        public float LookWeight { get; set; } = 1f;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetLookAtPosition(_aimTarget.transform.position);
            _animator.SetLookAtWeight(LookWeight);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _aimTarget.transform.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _aimWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }
    }
}