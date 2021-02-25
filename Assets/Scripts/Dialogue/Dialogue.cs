using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;
    [SerializeField] DialogueChoice[] choices = null;

    public override void SelectNode()
    {
        DialogueSystem.Instance.StartDialogue(messages, choices);
    }
}