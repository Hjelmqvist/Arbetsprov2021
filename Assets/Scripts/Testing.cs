using UnityEngine;

public class Testing : MonoBehaviour
{
    public CharacterBase charBase = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveManager.OnCaptureState?.Invoke();
            SaveManager.SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.LoadGame();
        }
    }
}
