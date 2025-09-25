using UnityEngine;

[CreateAssetMenu(menuName = "WhackASeal/Sound Bank")]
public class SoundBank : ScriptableObject
{
    [Header("Music")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 0.6f;

    [Header("SFX")]
    public AudioClip sealHitSfx;
    public AudioClip polarbeatHitSfx;
    public AudioClip popUpSfx;
    public AudioClip buttonClick;
    [Range(0f, 1f)] public float sfxVolume = 1f;
}