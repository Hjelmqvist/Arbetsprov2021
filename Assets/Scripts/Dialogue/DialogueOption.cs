using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Dialogues/Option")]
public class DialogueOption : DialogueNode
{
    [SerializeField] string message = "";
    [SerializeField] DialogueNode connectingDialogue = null;
    // [SerializeField] Requirement[] requirements = null;

    public override void Execute()
    {
        if (connectingDialogue)
            connectingDialogue.Execute();
    }

    //public bool Accessible()
    //{
    //    return requirements.All(x => x.fulfilled);
    //}
}