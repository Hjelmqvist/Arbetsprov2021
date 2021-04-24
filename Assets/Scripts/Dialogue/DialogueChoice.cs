using System;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeField, TextArea(2, 5)] string message = "";
    [SerializeField] DialogueNode connectingDialogue = null;
    [Tooltip("How to hide the choice when requirements are not fulfilled.")]
    [SerializeField] HideType hideType = HideType.NotInteractable;
    [SerializeField] Requirement requirements = null;
    [SerializeField] Reward rewards = null;

    public enum HideType { Hide, NotInteractable }

    public string Message { get { return message; } }
    //TODO: Think of a better name?
    public HideType HowToHide => hideType;

    public delegate void ChoiceFailed(string error);
    public static ChoiceFailed OnChoiceFailed;

    public void SelectChoice(GameObject user)
    {
        if (rewards.TryGiveRewards(user, out string error))
        {
            requirements.FulfillRequirements(user);
            connectingDialogue.SelectNode(user);
        }
        else
            OnChoiceFailed?.Invoke(error);
    }

    public bool CanFulfillRequirements(GameObject user)
    {
        return connectingDialogue && requirements.CanFulfillRequirements(user);
    }
}