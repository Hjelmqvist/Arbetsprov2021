using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;
    [SerializeField] DialogueChoice[] choices = null;

    public delegate void DialogueStart(DialogueMessage[] messages, DialogueChoice[] choices, GameObject user);
    public static event DialogueStart OnDialogueStart;

    public override void SelectNode(GameObject user)
    {
        OnDialogueStart?.Invoke(messages, choices, user);
    }
}