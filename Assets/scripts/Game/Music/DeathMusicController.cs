using UnityEngine;

public class DeathMusicController : MonoBehaviour

{
    [Header("Audio Sources")]
    public AudioSource musicSource; // Background/boss music
    public AudioSource sfxSource;   // Death sound effects

    [Header("Clips")]
    public AudioClip deathClip;

    // Called when player dies
    public void HandlePlayerDeath()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        if (sfxSource != null && deathClip != null)
        {
            sfxSource.clip = deathClip;
            sfxSource.loop = false;
            sfxSource.Play();
        }
    }

    // Called by BossSpawnTrigger to play boss theme
    public void PlayBossMusic(AudioClip bossClip)
    {
        if (musicSource != null && bossClip != null)
        {
            musicSource.clip = bossClip;
            musicSource.loop = true; // Keep looping during boss fight
            musicSource.Play();
        }
    }
}