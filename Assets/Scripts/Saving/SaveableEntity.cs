using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SaveableEntity : MonoBehaviour
{
    [Tooltip("Only modify the identifier to:" +
        "\n1. Make multiple entities share saved data, or" +
        "\n2. To make it easier to find certain data in the savefile")]
    [SerializeField] string uniqueIdentifier = "";

    private void Reset()
    {
        if (string.IsNullOrEmpty(uniqueIdentifier))
            uniqueIdentifier = System.Guid.NewGuid().ToString();
    }

    private void OnEnable()
    {
        SaveManager.OnCaptureState += OnCaptureState;
        SaveManager.OnSavefileLoaded += OnRestoreState;
    }

    private void OnDisable()
    {
        SaveManager.OnCaptureState -= OnCaptureState;
        SaveManager.OnSavefileLoaded -= OnRestoreState;
    }

    private void OnCaptureState()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            data[saveable.GetType().ToString()] = saveable.CaptureState();
        }
        SaveManager.CaptureState(uniqueIdentifier, data);
    }

    private void OnRestoreState()
    {
        if (SaveManager.TryRestoreState(uniqueIdentifier, out Dictionary<string, object> data))
        {
            foreach (ISaveable saveable in GetComponents<ISaveable>()) 
            {
                string type = saveable.GetType().ToString();
                if (data.ContainsKey(type))
                {
                    saveable.RestoreState(data[type]);
                }
            }
        }
    }
}
