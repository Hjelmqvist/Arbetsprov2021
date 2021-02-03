using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue")]
public class Dialogue : DialogueNode
{
    [SerializeField] DialogueMessage[] messages = null;

    [Header("Showed in reverse order."), Space(10)]
    [Tooltip("Reverse order so that new choices shows up at the top, like new quests being added")] 
    [SerializeField] DialogueChoice[] choices = null;

    public override void SelectNode()
    {
        DialogueSystem.Instance.StartDialogue(messages, choices);
    }
}