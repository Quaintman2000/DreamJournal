using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    // The current instance of the note manager.
    public static NoteManager Instance;
    // The inputfield UI
    public TMPro.TMP_InputField InputField;
    // Start is called before the first frame update
    void Awake()
    {
        // If the instance is not null...
        if(Instance !=null)
        {
            // Destroy that object and set it the instance to this.
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            // Set the instance to this.
            Instance = this;
        }
    }

    private void Start()
    {
        // If we're loading a file...
        if(ImageSaveAndLoad.LoadingFile == true)
        {
            // Set the inputfields text to the text the file has.
            InputField.text = ImageSaveAndLoad.LoadNote(ImageSaveAndLoad.fileToLoad);
        }
    }


}
