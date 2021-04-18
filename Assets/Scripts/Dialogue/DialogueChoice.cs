using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeField, TextArea(2, 5)] string message = "";
    [SerializeField] DialogueNode connectingDialogue = null;
    [SerializeField] Requirement requirements = null;

    public string Message { get { return message; } }

    public void SelectChoice(GameObject user)
    {
        if (connectingDialogue)
        {
            Debug.Log("Choice selected");
            requirements.FulfillRequirements(user);
            connectingDialogue.SelectNode(user);
        }      
    }

    public bool IsRequirementsFulfilled(GameObject user)
    {
        return connectingDialogue && requirements.IsFulfilled(user);
    }
}