using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;
    [SerializeField] DialogueChoice[] choices = null;

    public override void Execute()
    {
        DialogueSystem.Instance.StartDialogue(messages, choices);
    }
}