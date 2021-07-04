// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private byte _doorLevel = 1;
        public byte DoorLevel { get { return _doorLevel; } }
        private int _doorOpenAP = Animator.StringToHash("isDoorOpen");
        private int _doorOpenSpeedAP = Animator.StringToHash("doorSpeed");
        private Animator _animator;
        private WaitForSeconds _closeDelay;

        private void Start()
        {
            _animator = GetComponentInParent<Animator>();
            _closeDelay = new WaitForSeconds(5.0f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider != null)
            {
                var iCanOpenDoor = other.collider.GetComponent<ICanOpenDoor>();
                if(iCanOpenDoor != null)
                {
                    Debug.Log("Who disturbs my slumber!");
                    if (iCanOpenDoor.MaxDoorLevel >= _doorLevel)
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