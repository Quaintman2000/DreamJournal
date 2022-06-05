using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="UI Manager",menuName = "UI/UI Manager")]
public class UIManager : ScriptableObject
{
    /// <summary>
    /// Quits the program.
    /// </summary>
    public void Quit()
    {
        ImageSaveAndLoad.fileToLoad = string.Empty;
        ImageSaveAndLoad.LoadingFile = false;
        Application.Quit();
    }
    /// <summary>
    /// Opens the google webpage.
    /// </summary>
    public void SearchButtonClick()
    {
        Application.OpenURL("https://scholar.google.com/");
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnSaveConfirmClicked()
    {
        // Sves the canvas.
        ImageSaveAndLoad.SaveCanvas(LayerSystem.Instance.GetCanvas().name, LayerSystem.Instance.GetCanvas());

    }
    /// <summary>
    /// Opens a new drawing scene.
    /// </summary>
    public void OnDrawClicked()
    {
        ImageSaveAndLoad.fileToLoad = string.Empty;
        ImageSaveAndLoad.LoadingFile = false;
    }
    /// <summary>
    /// Saves the text file.
    /// </summary>
    public void OnSaveTextConfirm()
    {
        // Get a random number to name the file.
        int seed = DateTime.Now.Date.Second + DateTime.Now.Date.Day + DateTime.Now.Date.Month + DateTime.Now.Date.Year;
        UnityEngine.Random.InitState(seed);
        int randomNum = UnityEngine.Random.Range(1000, 9999);
        // Saves the file with the new name.
        ImageSaveAndLoad.SaveNote("Note"+ randomNum, NoteManager.Instance.InputField.text);
    }
 
}
