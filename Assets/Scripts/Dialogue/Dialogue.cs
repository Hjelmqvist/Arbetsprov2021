using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;

    [Header("Showed in reverse order."), Space(10)]
    [Tooltip("Reverse order so that new choices shows up at the top, like new quests being added")] 
    [SerializeField] DialogueChoice[] choices = null;

    public override void Execute()
    {
        DialogueSystem.Instance.StartDialogue(messages, choices);
    }
}