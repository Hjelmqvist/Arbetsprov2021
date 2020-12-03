using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The class to take care of the dialogue UI.
/// </summary>
public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    [SerializeField] GameObject dialoguePanel = null;
    [SerializeField] TextMeshProUGUI characterNameBox = null;
    [SerializeField] TextMeshProUGUI messageBox = null;
    [SerializeField] Transform buttonsParent = null;
    [SerializeField] Button choiceButtonPrefab = null;

    [SerializeField] float printSpeed = 0.1f;

    Queue<DialogueMessage> dialogueMessages = new Queue<DialogueMessage>();
    List<DialogueChoice> dialogueChoices = new List<DialogueChoice>();

    Action OnEndOfDialogue = null;
    Action OnPrintCompleted = null;

    bool exitDialogue = false;

    /// <summary>
    /// Setup and play dialogue
    /// </summary>
    public void StartDialogue(DialogueMessage[] messages, DialogueChoice[] choices)
    {
        ClearDialogue();

        for (int i = 0; i < messages.Length; i++)
            dialogueMessages.Enqueue(messages[i]);
        dialogueChoices.AddRange(choices);

        NextDialogue();
        dialoguePanel.SetActive(true);
    }

    /// <summary>
    /// Plays the next dialogue message from the queue
    /// </summary>
    public void NextDialogue()
    {
        if (dialogueMessages.Count > 0)
        {
            ClearDialogue();

            DialogueMessage dialogue = dialogueMessages.Dequeue();
            characterNameBox.text = dialogue.character;

            // Only one print coroutine at a time
            StopAllCoroutines();
            StartCoroutine(PrintMessage(dialogue.message));

            // Load the buttons if it was the last dialogue message
            if (dialogueMessages.Count == 0)
                OnPrintCompleted += CreateButtons;
        }
        else
            OnEndOfDialogue?.Invoke();
    }

    /// <summary>
    /// Creates buttons for the dialogue choices
    /// </summary>
    private void CreateButtons()
    {
        OnPrintCompleted -= CreateButtons;
        for (int i = dialogueChoices.Count - 1; i >= 0; i--)
        {
            DialogueChoice choice = dialogueChoices[i];
            Button newButton = Instantiate(choiceButtonPrefab, buttonsParent);
            newButton.onClick.AddListener(choice.Execute);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"\"{choice.Message}\"";
        }
        dialogueChoices.Clear();
    }

    /// <summary>
    /// Loads in the last couple messages and closes the dialogue window when they are done
    /// </summary>
    public void EndDialogue(DialogueMessage[] messages)
    {
        OnEndOfDialogue += CloseDialogue;
        CloseDialogue();
        dialogueMessages.Clear();
        for (int i = 0; i < messages.Length; i++)
            dialogueMessages.Enqueue(messages[i]);
        NextDialogue();
    }

    /// <summary>
    /// Close the dialogue window
    /// </summary>
    private void CloseDialogue()
    {
        OnEndOfDialogue -= CloseDialogue;
        dialoguePanel.SetActive(false);
    }

    /// <summary>
    /// Resets all UI elements
    /// </summary>
    private void ClearDialogue()
    {
        characterNameBox.text = "";
        messageBox.text = "";

        // Remove all buttons
        for (int i = buttonsParent.childCount - 1; i >= 0; i--)
            Destroy(buttonsParent.GetChild(i).gameObject);
    }

    IEnumerator PrintMessage(string message)
    {
        messageBox.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            messageBox.text += message[i];
            yield return new WaitForSeconds(printSpeed);
        }
        OnPrintCompleted?.Invoke();
    }
}