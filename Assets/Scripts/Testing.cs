using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public SpecialList list = null;
    public CharacterBase charBase = null;

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
    }
}

[System.Serializable]
public class SpecialList : List<string>
{

}