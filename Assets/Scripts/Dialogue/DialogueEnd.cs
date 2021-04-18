using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Ending", menuName = "Dialogues/End")]
public class DialogueEnd : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;

    public delegate void DialogueEnded(DialogueMessage[] messages, GameObject user);
    public static event DialogueEnded OnDialogueEnd;

    public override void SelectNode(GameObject user)
    {
        OnDialogueEnd?.Invoke(messages, user);
    }
}