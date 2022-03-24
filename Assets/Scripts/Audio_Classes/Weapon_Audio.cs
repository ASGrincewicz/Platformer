using UnityEngine;

public class Weapon_Audio: Audio_Manager
{
    [SerializeField] private AudioClip _primaryWeaponFireSound;
    [SerializeField] private AudioClip _secondaryWeaponFireSound;
    public AudioClip PrimaryWeaponFireSound { get => _primaryWeaponFireSound; }
    public AudioClip SecondaryWeaponFireSound { get => _secondaryWeaponFireSound; }
}
