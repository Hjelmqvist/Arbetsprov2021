﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Takes care of the dialogue window, including choice buttons
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel = null;
    [SerializeField] TextMeshProUGUI characterNameBox = null;
    [SerializeField] TextMeshProUGUI messageBox = null;
    [SerializeField] Button nextButton = null;
    [SerializeField] Transform buttonsParent = null;
    [SerializeField] Button choiceButtonPrefab = null;

    [SerializeField] float printSpeed = 0.1f;

    Queue<DialogueMessage> dialogueMessages = new Queue<DialogueMessage>();
    List<DialogueChoice> dialogueChoices = new List<DialogueChoice>();

    public static Action OnDialogueStart = null;
    public static Action OnDialogueEnd = null;
    Action OnPrintCompleted = null;

    GameObject currentUser = null;

    private void OnEnable()
    {
        Dialogue.OnDialogueStart += Dialogue_OnDialogueStart;
        DialogueEnd.OnDialogueEnd += DialogueEnd_OnDialogueEnd;
        DialogueChoice.OnChoiceFailed += DialogueChoice_OnChoiceFailed;
        OnDialogueEnd += CloseDialogue;
    }

    private void OnDisable()
    {
        Dialogue.OnDialogueStart -= Dialogue_OnDialogueStart;
        DialogueEnd.OnDialogueEnd -= DialogueEnd_OnDialogueEnd;
        DialogueChoice.OnChoiceFailed -= DialogueChoice_OnChoiceFailed;
        OnDialogueEnd -= CloseDialogue;
    }

    private void Dialogue_OnDialogueStart(DialogueMessage[] messages, DialogueChoice[] choices, GameObject user)
    {
        currentUser = user;
        ResetUI();
        LoadDialogue(messages, choices);
        ContinueDialogue();
        OnDialogueStart?.Invoke();
    }

    private void DialogueEnd_OnDialogueEnd(DialogueMessage[] messages, GameObject user)
    {
        LoadDialogue(messages, null);
        ContinueDialogue();
    }

    private void DialogueChoice_OnChoiceFailed(string error)
    {
        //Just end the dialogue if the choice didnt work for some reason.
        OnDialogueEnd?.Invoke();
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    /// <summary>
    /// Plays the next dialogue message from the queue
    /// </summary>
    public void ContinueDialogue()
    {
        if (dialogueMessages.Count > 0)
        {
            ResetUI();
            dialoguePanel.SetActive(true);

            DialogueMessage dialogue = dialogueMessages.Dequeue();
            characterNameBox.text = dialogue.character;

            // Load the buttons if it was the last dialogue message
            if (dialogueMessages.Count == 0)
                OnPrintCompleted += CreateButtons;

            nextButton.gameObject.SetActive(dialogueMessages.Count > 0 || dialogueChoices.Count == 0);

            // Only one print coroutine at a time
            StopAllCoroutines();
            StartCoroutine(PrintMessage(dialogue.message));
        }
        else if (dialogueChoices.Count == 0)
            OnDialogueEnd?.Invoke();
    }

    private void LoadDialogue(DialogueMessage[] messages, DialogueChoice[] choices)
    {
        dialogueMessages.Clear();
        dialogueChoices.Clear();
        if (messages != null)
        {
            for (int i = 0; i < messages.Length; i++)
                dialogueMessages.Enqueue(messages[i]);
        }
        if (choices != null)
            dialogueChoices.AddRange(choices);
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

            if (!choice.CanFulfillRequirements(currentUser) && choice.HowToHide == DialogueChoice.HideType.Hide)
                continue;

            Button newButton = Instantiate(choiceButtonPrefab, buttonsParent);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.Message;

            if (choice.CanFulfillRequirements(currentUser))
                newButton.onClick.AddListener(() => choice.SelectChoice(currentUser));
            else
                newButton.interactable = false;
        }
        dialogueChoices.Clear();
    }

    /// <summary>
    /// Resets all UI elements
    /// </summary>
    private void ResetUI()
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