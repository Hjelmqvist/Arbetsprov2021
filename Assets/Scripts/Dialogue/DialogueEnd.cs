using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/End")]
public class DialogueEnd : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;

    public override void Execute()
    {
        DialogueSystem.Instance.ExitDialogue(messages);
    }
}