using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ImageSaveAndLoad
{
    public static bool LoadOldCanvas = false;
    public static string fileToLoad;
    static Vector2 textureSize = new Vector2(696, 563);
    public static void SaveCanvas(string fileName, ArtCanvas artCanvas)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".dream";
        Debug.Log(path);
        FileStream stream;

        stream = new FileStream(path, FileMode.Create);


        formatter.Serialize(stream, artCanvas);
        stream.Close();
    }

    public static ArtCanvas LoadArtCanvas(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ArtCanvas data = formatter.Deserialize(stream) as ArtCanvas;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static string[] GetFileNames()
    {
        string path = Application.persistentDataPath + "/";

        string[] fileNames = Directory.GetFiles(path);

        for (int i = 0; i < fileNames.Length; i++)
        {
            fileNames[i] = fileNames[i].Replace(path, string.Empty);
        }

        return fileNames;

    }
    public static void SaveNote(string fileName, string note)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".note";
        Debug.Log(path);
        FileStream stream;

        stream = new FileStream(path, FileMode.Create);


        formatter.Serialize(stream, note);
        stream.Close();
    }

    public static string LoadNote(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
