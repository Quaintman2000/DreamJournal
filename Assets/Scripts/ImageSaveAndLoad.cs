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
        string path = Application.persistentDataPath + "/Canvases/" + fileName + ".dream";
        FileStream stream;
        if (!Directory.Exists(path))
        {
            stream = new FileStream(path, FileMode.Create);
        }
        else
        {
            stream = new FileStream(path, FileMode.Append);
        }

        formatter.Serialize(stream, artCanvas);
        stream.Close();
    }

    public static ArtCanvas LoadArtCanvas(string fileName)
    {
        string path = Application.persistentDataPath + "/Canvases/" + fileName + ".dream";
        if(File.Exists(path))
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
}
