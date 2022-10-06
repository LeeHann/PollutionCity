using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle masterToggle, BGMToggle, SFXToggle;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    public void Start()
    {
        if (0 == EncryptedPlayerPrefs.GetInt("MasterSoundIsOn", 1))
            MasterSoundVolume(0);
        if (0 == EncryptedPlayerPrefs.GetInt("BGMSoundIsOn", 1))
            BGMSoundVolume(0);
        if (0 == EncryptedPlayerPrefs.GetInt("SFXSoundIsOn", 1))
            SFXSoundVolume(0);
        
        masterSlider.value = EncryptedPlayerPrefs.GetFloat("MasterSoundVolume", 1.0f);
        BGMSlider.value = EncryptedPlayerPrefs.GetFloat("BGMSoundVolume", 1.0f);              audioMixer.SetFloat("BGMSoundVolume", Mathf.Log10(BGMSlider.value) * 20);
        SFXSlider.value = EncryptedPlayerPrefs.GetFloat("SFXSoundVolume", 1.0f);            audioMixer.SetFloat("SFXSoundVolume", Mathf.Log10(SFXSlider.value) * 20);
    }

    public void MasterSoundVolume(float val)
    {
        audioMixer.SetFloat("MasterSoundVolume", val > 0 ? Mathf.Log10(val) * 20 : -80.0f);
        EncryptedPlayerPrefs.SetFloat("MasterSoundVolume", val);
        EncryptedPlayerPrefs.SetInt("MasterSoundIsOn", val > 0 ? 1 : 0);

        if (masterToggle.isOn == false) {
            masterToggle.isOn = true;
        }
        if (val == 0f) {
            masterToggle.isOn = false;
        }
    }

    public void MasterSoundToggle(bool isOn){
        EncryptedPlayerPrefs.SetInt("MasterSoundIsOn", isOn ? 1 : 0);
        audioMixer.SetFloat("MasterSoundVolume", isOn ? (masterSlider.value > 0 ? Mathf.Log10(masterSlider.value) * 20 : -80.0f) : -80.0f);
    }

    public void BGMSoundVolume(float val)
    {
        audioMixer.SetFloat("BGMSoundVolume", val > 0 ? Mathf.Log10(val) * 20 : -80.0f);
        EncryptedPlayerPrefs.SetFloat("BGMSoundVolume", val);
        EncryptedPlayerPrefs.SetInt("BGMSoundIsOn", val > 0 ? 1 : 0);
        
        if(BGMToggle.isOn == false){
            BGMToggle.isOn = true;
        }
        if (val == 0f) {
            BGMToggle.isOn = false;
        }
    }

    public void BGMSoundToggle(bool isOn){
        EncryptedPlayerPrefs.SetInt("BGMSoundIsOn", isOn ? 1 : 0);
        audioMixer.SetFloat("BGMSoundVolume", isOn ? (BGMSlider.value > 0 ? Mathf.Log10(BGMSlider.value) * 20 : -80.0f) : -80.0f);
    }

    public void SFXSoundVolume(float val)
    {
        audioMixer.SetFloat("SFXSoundVolume", val > 0 ? Mathf.Log10(val) * 20 : -80.0f);
        EncryptedPlayerPrefs.SetFloat("SFXSoundVolume", val);
        EncryptedPlayerPrefs.SetInt("SFXSoundIsOn", val > 0 ? 1 : 0);
        
        if(SFXToggle.isOn == false){
            SFXToggle.isOn = true;
        }
        if (val == 0f) {
            SFXToggle.isOn = false;
        }
    }

    public void SFXSoundToggle(bool isOn){
        EncryptedPlayerPrefs.SetInt("SFXSoundIsOn", isOn ? 1 : 0);
        audioMixer.SetFloat("SFXSoundVolume", isOn ? (SFXSlider.value > 0 ? Mathf.Log10(SFXSlider.value) * 20 : -80.0f) : -80.0f);
    }
}
