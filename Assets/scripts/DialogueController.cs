using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;

    private void Start()
    {
        // Hide panel at the start
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string[] lines)
    {
        StopAllCoroutines();
        StartCoroutine(RunDialogue(lines));
    }

    private IEnumerator RunDialogue(string[] lines)
    {
        dialoguePanel.SetActive(true);

        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeSentence(line));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        dialoguePanel.SetActive(false); // Hide after dialogue ends
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
