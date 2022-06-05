using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ImageSaveAndLoad
{
    // A bool to indicate we're loading a file.
    public static bool LoadingFile = false;
    // The name of the file to load.
    public static string fileToLoad;
    // The texture size of the canvas.
    static Vector2 textureSize = new Vector2(696, 563);
    /// <summary>
    /// Saves the canvas into the directory.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="artCanvas">The canvas to save.</param>
    public static void SaveCanvas(string fileName, ArtCanvas artCanvas)
    {
        // Creates a formatter to save the file.
        BinaryFormatter formatter = new BinaryFormatter();
        // Save the path of the file.
        string path = Application.persistentDataPath + "/" + fileName + ".dream";
        Debug.Log(path);
        // Create a new filestream.
        FileStream stream;
        // Set the stream to create a new file at the set path.
        stream = new FileStream(path, FileMode.Create);
        // Serialize the file.
        formatter.Serialize(stream, artCanvas);
        // Close the stream.
        stream.Close();
    }
    /// <summary>
    /// Loads and returns the artcanvas from a specified file.
    /// </summary>
    /// <param name="fileName">name of the file.</param>
    /// <returns>Returns the art canvas from that file.</returns>
    public static ArtCanvas LoadArtCanvas(string fileName)
    {
        // Get the file path.
        string path = Application.persistentDataPath + "/" + fileName;
        // If a file exists there...
        if (File.Exists(path))
        {
            // Make a formatter.
            BinaryFormatter formatter = new BinaryFormatter();
            // Make a new stream to open a file at that path.
            FileStream stream = new FileStream(path, FileMode.Open);
            // Deserialize the file to get the art canvas.
            ArtCanvas data = formatter.Deserialize(stream) as ArtCanvas;
            // Close the stream.
            stream.Close();
            // Return the canvas/
            return data;
        }
        else
        {
            // Return null.
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    /// <summary>
    /// Gets the file names from the current directory.
    /// </summary>
    /// <returns>An array of file names.</returns>
    public static string[] GetFileNames()
    {
        // Get the directory path.
        string path = Application.persistentDataPath + "/";
        // Gets the array of all the file names (including their paths)
        string[] fileNames = Directory.GetFiles(path);
        // Trim all the names in the array to not have their paths.
        for (int i = 0; i < fileNames.Length; i++)
        {
            fileNames[i] = fileNames[i].Replace(path, string.Empty);
        }
        // Return the array of file names.
        return fileNames;

    }
    /// <summary>
    /// Saves the note.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="note">The note itself.</param>
    public static void SaveNote(string fileName, string note)
    {
        // Create a formatter.
        BinaryFormatter formatter = new BinaryFormatter();
        // Set the file path.
        string path = Application.persistentDataPath + "/" + fileName + ".note";
        Debug.Log(path);
        // Make a stream.
        FileStream stream;
        // Create a stream to make a new file at that path.
        stream = new FileStream(path, FileMode.Create);
        // Serialize the file to save it.
        formatter.Serialize(stream, note);
        // Close the stream.
        stream.Close();
    }
    /// <summary>
    /// Loads the note file.
    /// </summary>
    /// <param name="fileName">The name of the note file.</param>
    /// <returns></returns>
    public static string LoadNote(string fileName)
    {
        // Set the path.
        string path = Application.persistentDataPath + "/" + fileName;
        // If the file exists at that path...
        if (File.Exists(path))
        {
            // Create a formatter and a stream to open a file at that path.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // Deserialize the file to get the note and close the stream.
            string data = formatter.Deserialize(stream) as string;
            stream.Close();
            // Return the note.
            return data;
        }
        else
        {
            // Return null.
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
