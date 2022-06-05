using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="UI Manager",menuName = "UI/UI Manager")]
public class UIManager : ScriptableObject
{
    public void Quit()
    {
        ImageSaveAndLoad.fileToLoad = string.Empty;
        ImageSaveAndLoad.LoadOldCanvas = false;
        Application.Quit();
    }

    public void SearchButtonClick()
    {
        Application.OpenURL("https://scholar.google.com/");
    }

    public void OnSaveConfirmClicked()
    {
        int seed = DateTime.Now.Date.Second + DateTime.Now.Date.Day + DateTime.Now.Date.Month + DateTime.Now.Date.Year;
        UnityEngine.Random.InitState(seed);

        ImageSaveAndLoad.SaveCanvas(LayerSystem.Instance.GetCanvas().name, LayerSystem.Instance.GetCanvas());

    }

    public void OnDrawClicked()
    {
        ImageSaveAndLoad.fileToLoad = string.Empty;
        ImageSaveAndLoad.LoadOldCanvas = false;
    }
    
    public void OnSaveTextConfirm()
    {
        int seed = DateTime.Now.Date.Second + DateTime.Now.Date.Day + DateTime.Now.Date.Month + DateTime.Now.Date.Year;
        UnityEngine.Random.InitState(seed);
        int randomNum = UnityEngine.Random.Range(1000, 9999);

        ImageSaveAndLoad.SaveNote("Note"+ randomNum, NoteManager.Instance.InputField.text);
    }
 
}
