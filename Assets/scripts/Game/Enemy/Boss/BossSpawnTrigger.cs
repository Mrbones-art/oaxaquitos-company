using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject bossObject;           // Reference to the boss already in the scene
    public string[] bossIntroDialogue;      // Dialogue lines to play before/when boss appears

    [Header("Music Settings")]
    public AudioClip bossTheme;             // Boss battle music

    private bool hasTriggered = false;
    private DeathMusicController deathmusicController;

    private void Start()
    {
        // Find MusicController in scene
        deathmusicController = FindObjectOfType<DeathMusicController>();
        if (deathmusicController == null)
        {
            Debug.LogWarning("BossSpawnTrigger: No MusicController found in scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            EnableBossAndDialogue();
            PlayBossTheme();
            hasTriggered = true;
        }
    }

    void EnableBossAndDialogue()
    {
        if (bossObject != null)
        {
            bossObject.SetActive(true); // Enable the boss
        }
        else
        {
            Debug.LogWarning("BossSpawnTrigger: Boss object reference is missing.");
        }

        // Play dialogue if there’s a DialogueController in the scene
        DialogueController dialogueController = FindObjectOfType<DialogueController>();
        if (dialogueController != null && bossIntroDialogue.Length > 0)
        {
            dialogueController.ShowDialogue(bossIntroDialogue);
        }
    }

    void PlayBossTheme()
    {
        if (deathmusicController != null && bossTheme != null)
        {
            deathmusicController.PlayBossMusic(bossTheme);
        }
        else
        {
            Debug.LogWarning("BossSpawnTrigger: Missing MusicController or BossTheme clip.");
        }
    }
}
