using UnityEngine;

[System.Serializable]
public class DialogueMessage
{
    public string character;
    [TextArea(2, 5)]
    public string message;
}