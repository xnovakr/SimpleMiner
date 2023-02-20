using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    public static void SaveGame(int[][,] worldData, int saveIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pecok" + saveIndex + ".budzogan";
        FileStream stream = new FileStream(path, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);

        formatter.Serialize(stream, worldData);
        stream.Close();
    }

    public static int[][,] LoadGame(int saveIndex)
    {
        string path = Application.persistentDataPath + "/pecok" + saveIndex + ".budzogan";
        if (File.Exists(path))
        {
            int[][,] worldData = null;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            worldData = formatter.Deserialize(stream) as int[][,];
            stream.Close();
            return worldData;

        }else
        {
            Debug.Log("No File Found");
            return null;
        }
    }
    public static bool CheckSave(int index)
    {
        string path = Application.persistentDataPath + "/pecok" + index + ".budzogan";
        return CheckFile(path);
    }
    public static bool CheckFile(string path)
    {
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DeleteSave(int saveIndex)
    {
        File.Delete(Application.persistentDataPath + "/pecok" + saveIndex + ".budzogan");
    }

    public static bool SaveCheck(int index)
    {
        if (File.Exists(Application.persistentDataPath + "/pecok" + index + ".budzogan"))
        {
            return true;
        }
        else
        {
            Debug.Log("No File Found");
            return false;
        }
    }
}
