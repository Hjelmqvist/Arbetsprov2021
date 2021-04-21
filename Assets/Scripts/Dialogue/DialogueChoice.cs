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
    public HideType HowToHide => hideType;

    public static Action OnChoiceFailed;

    public void SelectChoice(GameObject user)
    {
        if (rewards.CanGiveRewards(user))
        {
            requirements.FulfillRequirements(user);
            rewards.GiveRewards(user);
            connectingDialogue.SelectNode(user);
        }
        else
            OnChoiceFailed?.Invoke();
    }

    public bool CanFulfillRequirements(GameObject user)
    {
        return connectingDialogue && requirements.CanFulfillRequirements(user);
    }
}