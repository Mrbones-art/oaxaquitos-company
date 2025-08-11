using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    public GameObject bossObject;           // Reference to the boss already in the scene
    public string[] bossIntroDialogue;      // Dialogue lines to play before/when boss appears

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            EnableBossAndDialogue();
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
}
