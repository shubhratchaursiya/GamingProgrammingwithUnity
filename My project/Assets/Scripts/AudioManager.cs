using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // ✅ Singleton (so you can call from anywhere)

    [Header("Audio Sources")]
    public AudioSource musicSource;   // For background music (loops)
    public AudioSource sfxSource;     // For sound effects (jump, coin, etc.)

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        // ✅ Make sure only one AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps playing across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ✅ Start background music if assigned
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic, true);
        }
    }

    // ✅ Play background music
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // ✅ Stop music
    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    // ✅ Play sound effects
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // ✅ Shortcut methods
    public void PlayJump() => PlaySFX(jumpSound);
    public void PlayCoin() => PlaySFX(coinSound);
    public void PlayGameOver() => PlaySFX(gameOverSound);
}

