using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip newTrack; // Music to play when triggered
    private AudioSource audioSource;

    private void Start()
    {
        // Find the AudioSource in the scene (MusicManager)
        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            if (audioSource.clip != newTrack) // Avoid restarting the same track
            {
                StartCoroutine(FadeAndChangeTrack(newTrack));
            }
        }
    }

    private System.Collections.IEnumerator FadeAndChangeTrack(AudioClip newClip)
    {
        // Fade out
        for (float v = 1f; v >= 0; v -= Time.deltaTime)
        {
            audioSource.volume = v;
            yield return null;
        }

        // Change and play
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float v = 0; v <= 1f; v += Time.deltaTime)
        {
            audioSource.volume = v;
            yield return null;
        }
    }
}
