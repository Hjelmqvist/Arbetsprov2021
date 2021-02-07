using UnityEngine;

public class Testing : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Saving");
            SaveManager.OnCaptureState?.Invoke();
            SaveManager.SaveFile();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.LoadFile();
        }
    }
}
