// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private byte _doorLevel = 1;
        private int _doorOpenAP = Animator.StringToHash("isDoorOpen");
        private int _doorOpenSpeedAP = Animator.StringToHash("doorSpeed");
        private ICanOpenDoor _iCanOpenDoor;
        private Animator _animator;
        private WaitForSeconds _closeDelay;
        public byte DoorLevel { get { return _doorLevel; } }

        private void Start()
        {
            _animator = GetComponentInParent<Animator>();
            _closeDelay = new WaitForSeconds(5.0f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider != null)
            {
                _iCanOpenDoor = other.collider.GetComponent<ICanOpenDoor>();
                if(_iCanOpenDoor != null)
                {
                   // Debug.Log("Who disturbs my slumber!");
                    if (_iCanOpenDoor.MaxDoorLevel >= _doorLevel)
                    {
                        _animator.SetFloat(_doorOpenSpeedAP, 1.0f);
                        _animator.SetBool(_doorOpenAP, true);
                        StartCoroutine(DoorCloseRoutine());
                    }
                }
            }
        }
        private IEnumerator DoorCloseRoutine()
        {
            yield return _closeDelay;
            _animator.SetFloat(_doorOpenSpeedAP, -1.0f);
            _animator.SetBool(_doorOpenAP, false);
        }
    }
}