using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FileLoader : MonoBehaviour
{
    [SerializeField]
    SceneLoader sceneLoader;
    [SerializeField]
    ScrollRect filesScrollRect;
    [SerializeField]
    Button fileButtonPrefab;


    private void Start()
    {
        string[] fileNames = ImageSaveAndLoad.GetFileNames();
        foreach(string fileName in fileNames)
        {
            Button newButton = Instantiate(fileButtonPrefab, filesScrollRect.content);
            newButton.GetComponentInChildren<Text>().text = fileName;
            newButton.onClick.AddListener(() => OnLoadClicked(fileName));
        }
    }

    public void OnLoadClicked(string fileName)
    {
        Debug.Log(fileName);
        ImageSaveAndLoad.fileToLoad = fileName;
        ImageSaveAndLoad.LoadOldCanvas = true;
        if(fileName.Contains("Note"))
        {
            sceneLoader.LoadScene("NotingScene");
        }
        else
        {

        sceneLoader.LoadScene("DrawingScene");
        }
    }
}
