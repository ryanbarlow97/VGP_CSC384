using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void Save(SaveData data, int slotNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/saveSlot{slotNumber}.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load(int slotNumber)
    {
        string path = Application.persistentDataPath + $"/saveSlot{slotNumber}.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

    public static bool SaveExists(int slotNumber)
    {
        string path = Application.persistentDataPath + $"/saveSlot{slotNumber}.dat";
        return File.Exists(path);
    }
    public static void Delete(int slotNumber)
    {
        string filePath = Application.persistentDataPath + $"/saveSlot{slotNumber}.dat";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
