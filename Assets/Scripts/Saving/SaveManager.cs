using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    const string SAVEFILENAME = "gamesave.save";

    static Dictionary<string, object> saveData = new Dictionary<string, object>();

    public static Action OnCaptureState;
    public static Action OnGameSaved;
    public static Action OnGameLoaded;

    public static void CaptureState(string id, object data)
    {
        if (!string.IsNullOrEmpty(id))
            saveData[id] = data;
    }

    public static bool TryRestoreState<T>(string id, out T data)
    {
        data = default;
        if (saveData.TryGetValue(id, out object value))
            data = (T)value;
        return data != null;
    }

    private static string GetSaveLocation()
    {
        return Path.Combine(Application.persistentDataPath, SAVEFILENAME);
    }

    public static void SaveGame()
    {
        OnCaptureState?.Invoke();

        using (FileStream stream = File.Open(GetSaveLocation(), FileMode.OpenOrCreate))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, saveData);
            OnGameSaved?.Invoke();
        }
    }

    public static bool LoadGame()
    {
        string filePath = GetSaveLocation();
        if (File.Exists(filePath))
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                saveData = (Dictionary<string, object>)formatter.Deserialize(stream);
            }
            OnGameLoaded?.Invoke();
            return true;
        }
        return false;
    }

    public static void DeleteSavefile()
    {
        string filePath = GetSaveLocation();
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}