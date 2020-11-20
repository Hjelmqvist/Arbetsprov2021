using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Quest")]
public class QuestOption : DialogueOption
{
    // [SerializeField] Quest quest = null;

    public override void Execute()
    {
        base.Execute();
        // Add quest
    }
}