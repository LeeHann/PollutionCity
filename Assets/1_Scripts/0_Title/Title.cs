using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private void Start() 
    {
        Screen.SetResolution(1920, 1080, false);
        Screen.fullScreen = false;
        DisableSystemUI.DisableNavUI();
    }

    public void OnClickStartButton()
    {
        Loading.LoadSceneHandle("World");
    }

    public void OnClickSettingButton()
    {

    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
