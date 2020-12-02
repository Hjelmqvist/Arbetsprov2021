using UnityEngine;
using System.Linq;

[System.Serializable]
public class DialogueChoice
{
    [SerializeField, TextArea(2, 5)] string message = "";
    [SerializeField] DialogueNode connectingDialogue = null;
    // [SerializeField] Quest quest = null;
    // [SerializeField] Requirement[] requirements = null;

    public string Message { get { return message; } }

    public void Execute()
    {
        if (connectingDialogue)
            connectingDialogue.Execute();

        ////if (quest)
        ////    quest.Start();
    }

    public bool Accessible()
    {
        return true;
        //return requirements.All(x => x.fulfilled);
    }
}