using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;
    public TMPro.TMP_InputField InputField;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance !=null)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(ImageSaveAndLoad.LoadOldCanvas == true)
        {
            InputField.text = ImageSaveAndLoad.LoadNote(ImageSaveAndLoad.fileToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
