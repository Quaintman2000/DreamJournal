using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FileLoader : MonoBehaviour
{
    // The sceneloader object to change scenes.
    [SerializeField]
    SceneLoader sceneLoader;
    // The scroll Rect UI where our buttons will spawn.
    [SerializeField]
    ScrollRect filesScrollRect;
    // The prefab for the buttons.
    [SerializeField]
    Button fileButtonPrefab;


    private void Start()
    {
        // Get an array of all of our file names.
        string[] fileNames = ImageSaveAndLoad.GetFileNames();
        // If we have saved files.
        if (fileNames != null)
        {
            // For each filename in the array...
            foreach (string fileName in fileNames)
            {
                // Create a new button.
                Button newButton = Instantiate(fileButtonPrefab, filesScrollRect.content);
                // Set the text to be the name of the file.
                newButton.GetComponentInChildren<Text>().text = fileName;
                // Set it so the onclick function calls the OnLoadClicked function with its file name.
                newButton.onClick.AddListener(() => OnLoadClicked(fileName));
            }
        }
    }

    /// <summary>
    /// Loads the specified file by name.
    /// </summary>
    /// <param name="fileName">Name of the file to load.</param>
    public void OnLoadClicked(string fileName)
    {
        // Load the file.
        ImageSaveAndLoad.fileToLoad = fileName;
        // Set that we're loading a new canvas.
        ImageSaveAndLoad.LoadingFile = true;
        // If this is a note file...
        if(fileName.Contains("Note"))
        {
            // Load the note scene.
            sceneLoader.LoadScene("NotingScene");
        }
        else
        {
            // Load the drawing scene.
        sceneLoader.LoadScene("DrawingScene");
        }
    }
}
