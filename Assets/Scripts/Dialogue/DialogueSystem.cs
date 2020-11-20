using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    [SerializeField] TextMeshProUGUI messageBox = null;
    [SerializeField] GameObject optionButtonPrefab = null;

    DialogueMessage currentMessage = null;

    public void PlayDialogue(DialogueMessage[] messages, DialogueOption[] options)
    {
        // Open dialogue window
        // Print letters
    }

    public void ExitDialogue(DialogueMessage[] messages)
    {
        // Close dialogue window
    }

    IEnumerator PrintMessage(string message)
    {
        messageBox.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            messageBox.text += message[i];
            yield return new WaitForFixedUpdate();
        }
    }
}