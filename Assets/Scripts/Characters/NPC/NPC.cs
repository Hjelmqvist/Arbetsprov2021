using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue npcDialogue = null;

    public void Interact(GameObject user)
    {
        if (npcDialogue)
            npcDialogue.SelectNode(user);
    }
}
