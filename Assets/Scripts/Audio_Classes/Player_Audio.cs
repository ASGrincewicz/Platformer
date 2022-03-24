using UnityEngine;

public class Player_Audio: Audio_Manager
{
    [SerializeField] private AudioClip _playerJumpSound;
    [SerializeField] private AudioClip _playerSlideSound;
    [SerializeField] private AudioClip _playerRunSound;
    [SerializeField] private AudioClip _playerWeaponSwitchSound;
    [SerializeField] private AudioClip _playerTransformSound;
    [SerializeField] private AudioClip _playerBombDropSound;
    [SerializeField] private AudioClip _playerGetCollectibleSound;
    [SerializeField] private AudioClip _playerUpgradeSound;
    public AudioClip PlayerJumpSound { get => _playerJumpSound; }
    public AudioClip PlayerSlideSound { get => _playerSlideSound; }
    public AudioClip PlayerRunSound { get => _playerRunSound; }
    public AudioClip PlayerWeaponSwitchSound { get => _playerWeaponSwitchSound; }
    public AudioClip PlayerTransformSound { get => _playerTransformSound; }
    public AudioClip PlayerBombDropSound { get => _playerBombDropSound; }
    public AudioClip PlayerGetCollectibleSound { get => _playerGetCollectibleSound; }
    public AudioClip PlayerUpgradeSound { get => _playerUpgradeSound; }
}
