using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundBank bank;

    private AudioSource music;
    private AudioSource sfx;

    private bool muted;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        music = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        music.playOnAwake = false;

        sfx = gameObject.AddComponent<AudioSource>();
        sfx.loop = false;
        sfx.playOnAwake = false;
    }

    public void SetMuted(bool mute)
    {
        muted = mute;

        music.mute = mute;
        sfx.mute = mute;
    }

    public void PlayBackgroundMusic()
    {
        if(bank == false || bank.backgroundMusic == false || muted == true)
        {
            return;
        }

        music.clip = bank.backgroundMusic;
        music.volume = bank.musicVolume;

        if(music.isPlaying == false)
        {
            music.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        music.Stop();
    }

    public void PlaySfx(AudioClip clip, float? volume = null)
    {
        if(clip == false || muted == true)
        {
            return;
        }

        float v = volume.HasValue ? Mathf.Clamp01(volume.Value) : (bank ? bank.sfxVolume : 1f);
        sfx.PlayOneShot(clip, v);
    }

    public void PlaySealHit()
    {
        if(bank == true)
        {
            PlaySfx(bank.sealHitSfx);
        }
    }

    public void PlayPolarbearHit()
    {
        if(bank == true)
        {
            PlaySfx(bank.polarbeatHitSfx);
        }
    }

    public void PlayPopUp()
    {
        if(bank == true)
        {
            PlaySfx(bank.popUpSfx);
        }
    }
}
