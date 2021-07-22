// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private float _aimWeight = 1f;
        [SerializeField] private GameObject _aimTarget;
        private Animator _animator;
        public float AimWeight { get { return _aimWeight; } set { _aimWeight = value; } }
        public float LookWeight { get; set; } = 1f;

        private void Start() => _animator = GetComponent<Animator>();

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