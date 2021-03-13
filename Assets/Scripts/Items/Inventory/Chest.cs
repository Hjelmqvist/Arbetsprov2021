using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] Inventory inventory = null;

    public void Interact(GameObject interactor)
    {
        throw new System.NotImplementedException();
    }
}