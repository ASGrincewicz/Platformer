using UnityEngine;

public class Door_Audio: Audio_Manager
{
    [SerializeField] private AudioClip _doorUnlockSound;
    [SerializeField] private AudioClip _doorOpenSound;
    [SerializeField] private AudioClip _doorCloseSound;
    public AudioClip DoorUnlockSound { get => _doorUnlockSound; }
    public AudioClip DoorOpensound { get => _doorOpenSound; }
    public AudioClip DoorCloseSound { get => _doorCloseSound; }
}