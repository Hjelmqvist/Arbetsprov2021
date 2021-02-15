using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    const string SAVEFILENAME = "gamesave.save";

    static Dictionary<string, object> saveData = new Dictionary<string, object>();

    public static Action OnCaptureState;
    public static Action OnSavefileLoaded;

    public static void CaptureState(string id, object data)
    {
        if (!string.IsNullOrEmpty(id))
            saveData[id] = data;
    }

    public static bool TryRestoreState<T>(string id, out T data)
    {
        data = (T)saveData[id];
        return data != null;
    }

    public static string GetSaveLocation()
    {
        return $"{Application.persistentDataPath}/{SAVEFILENAME}";
    }

    public static void SaveFile()
    {
        using (FileStream file = File.Open(GetSaveLocation(), FileMode.OpenOrCreate))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, saveData);
        }
    }

    public static void LoadFile()
    {
        string filePath = GetSaveLocation();
        if (File.Exists(filePath))
        {
            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                saveData = (Dictionary<string, object>)formatter.Deserialize(file);
            }
            OnSavefileLoaded?.Invoke();
        }
    }

    public static void DeleteSavefile()
    {
        string filePath = GetSaveLocation();
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
