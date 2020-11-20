using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;
    [SerializeField] DialogueOption[] options = null;

    public override void Execute()
    {
        DialogueSystem.Instance.PlayDialogue(messages, options);
    }
}