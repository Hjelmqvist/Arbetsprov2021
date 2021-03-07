using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Action", menuName = "Dialogues/Dialogue Action")]
public class DialogueEvent : DialogueNode
{
    [SerializeField] UnityEvent actions = null;

    public override void SelectNode()
    {
        DialogueSystem.Instance.EndDialogue(null);
        actions.Invoke();
    }
}