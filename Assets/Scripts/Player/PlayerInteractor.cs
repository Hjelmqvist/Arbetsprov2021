﻿using UnityEngine;

/// <summary>
/// Component to be on the player character that looks for interactable objects
/// </summary>
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] KeyCode interactKey = default;
    [SerializeField] Vector3 boxSize = Vector3.one;

    bool canInteract = true;
    
    void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
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
        DialogueSystem.OnDialogueStart += OnDialogueStart;
        DialogueSystem.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable()
    {
        DialogueSystem.OnDialogueStart -= OnDialogueStart;
        DialogueSystem.OnDialogueEnd -= OnDialogueEnd;
    }

    private void OnDialogueStart()
    {
        canInteract = false;
    }

    private void OnDialogueEnd()
    {
        canInteract = true;
    }
}