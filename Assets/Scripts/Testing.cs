using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Requirement req = null;
    [SerializeField] GameObject player = null;

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
            Debug.Log(req.IsFulfilled(player));
        }
    }
}