// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using System.Collections;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class Door : MonoBehaviour
    {
        [SerializeField,Tooltip("Indicates if the door is locked.")]
        private bool _locked = false;
        [SerializeField, Tooltip("Only a projectile with a 'Door Level' at or above this number can open this door.")]
        private int _doorLevel = 1;
        [SerializeField, Tooltip("The materials to used for locked and unlocked states.")]
        private Material[] _doorMat = new Material[2];//0 is unlocked, 1 is locked.
        private int _doorOpenAP = Animator.StringToHash("isDoorOpen");
        private int _doorOpenSpeedAP = Animator.StringToHash("doorSpeed");
        private ICanOpenDoor _iCanOpenDoor;
        private Animator _animator;
        private MeshRenderer _meshRenderer;
        private WaitForSeconds _closeDelay;
        public int DoorLevel { get { return _doorLevel; } }

        private void Start()
        {
            _animator = GetComponentInParent<Animator>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _closeDelay = new WaitForSeconds(5.0f);
            if (_locked)
                _meshRenderer.material = _doorMat[1];
            else
                _meshRenderer.material = _doorMat[0];
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider != null)
            {
                _iCanOpenDoor = other.collider.GetComponent<ICanOpenDoor>();
                if(_iCanOpenDoor != null)
                {
                    if (_iCanOpenDoor.MaxDoorLevel >= _doorLevel)
                    {
                        if(_locked)
                        {
                            _locked = false;
                            _doorLevel = 1;
                            _meshRenderer.material = _doorMat[0];
                        }
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