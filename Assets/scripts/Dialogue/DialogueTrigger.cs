using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines; // Lines shown when triggered
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            FindObjectOfType<DialogueController>().ShowDialogue(dialogueLines);
            hasTriggered = true;
        }
    }
}
