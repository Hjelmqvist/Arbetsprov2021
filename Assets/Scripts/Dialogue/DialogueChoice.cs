using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeField, TextArea(2, 5)] string message = "";
    [SerializeField] DialogueNode connectingDialogue = null;

    public string Message { get { return message; } }

    public void Execute()
    {
        if (connectingDialogue)
            connectingDialogue.SelectNode();
    }
}