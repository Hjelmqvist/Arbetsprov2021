using UnityEngine;

[System.Serializable]
public class DialogueMessage
{
    public string character;
    [TextArea(2, 5)]
    public string message;

    //Could add strings for character animations, camera transitions etc. to be sent out with static event
}