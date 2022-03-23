using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.Log($"{this} Audio Source is null!");
    }

    public virtual void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
