using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Ending", menuName = "Dialogues/End")]
public class DialogueEnd : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;

    public override void SelectNode()
    {
        DialogueSystem.Instance.EndDialogue(messages);
    }
}