using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager
{
    public static void SaveXML<T>(T data, string filename)
    {
        string filePath = Application.persistentDataPath + "/" + filename;

        //XML doesn't like opening existing files for some reason
        //if (File.Exists(filePath))
        //    File.Delete(filePath);

        using (FileStream file = File.Open(filePath, FileMode.OpenOrCreate))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(file);
            serializer.Serialize(writer, data);
            writer.Close();
        }
    }

    public static bool TryLoadXML<T>(string filename, out T data)
    {
        string filePath = Application.persistentDataPath + "/" + filename;
        if (File.Exists(filePath))
        {
            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                TextReader reader = new StreamReader(file);
                data = (T)serializer.Deserialize(reader);
                reader.Close();
            }
        }
        data = default;
        return data != null;
    }
}
