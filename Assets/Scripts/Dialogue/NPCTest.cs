using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour//, IInteractable
{
    [SerializeField] Dialogue dialogue = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            dialogue.Execute();
    }
}
