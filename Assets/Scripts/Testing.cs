using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public PlayerInventory inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveManager.SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
           
        }
    }
}