using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject setting;
    [SerializeField] TextMeshProUGUI bi;
    [SerializeField] AudioSource audioSource;
    
    private void Start() 
    {
        Screen.SetResolution(1920, 1080, false);
        DisableSystemUI.DisableNavUI();
    }

    public void OnClickStartButton()
    {
        StartCoroutine(StartSFX());
    }

    public void OnClickSettingButton()
    {
        mainPanel.SetActive(false);
        setting.SetActive(true);
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator StartSFX()
    {
        float value = 0.5f;
        float time = 0f;
        Color color = bi.color;
        
        audioSource.Play();
        while (value > -1f)
        {
            time += Time.deltaTime;

            value = Mathf.Lerp(0.5f, -1f, time);
            bi.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, value);
            
            color.a = Mathf.Lerp(1f, 0f, time);
            bi.color = color;

            audioSource.volume = Mathf.Lerp(1f, 0f, time);
            yield return null;
        }
        
        Loading.LoadSceneHandle("World");
    }
}
