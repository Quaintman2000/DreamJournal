using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UI Manager",menuName = "UI/UI Manager")]
public class UIManager : ScriptableObject
{
    public void Quit()
    {
        Application.Quit();
    }

    public void SearchButtonClick()
    {
        Application.OpenURL("https://scholar.google.com/");
    }
    
}
