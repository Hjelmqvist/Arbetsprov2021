using UnityEngine;

/// <summary>
/// Component to be on the player character that looks for interactable objects
/// </summary>
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] string interactButton = "Interact";
    [SerializeField] Vector3 boxSize = Vector3.one;

    bool canInteract = true;
    
    void Update()
    {
        //TODO: Interactable list?
        //Interact with first interactable when key is pressed down
        if (canInteract && Input.GetButtonDown(interactButton))
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxSize / 2, transform.forward);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(gameObject);
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        DialogueSystem.OnDialogueStart += DisableInteracting;
        DialogueSystem.OnDialogueEnd += EnableInteracting;
    }

    private void OnDisable()
    {
        DialogueSystem.OnDialogueStart -= DisableInteracting;
        DialogueSystem.OnDialogueEnd -= EnableInteracting;
    }

    private void EnableInteracting()
    {
        canInteract = true;
    }

    private void DisableInteracting()
    {
        canInteract = false;
    }
}