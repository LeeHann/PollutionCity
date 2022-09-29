using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGSound : MonoBehaviour
{
    public float duration = 1f;
    public AudioSource audioSource;
    public AudioClip[] bgmClips;

    private void Start() 
    {
        DontDestroyOnLoad(this);
        this.gameObject.name += " (Singleton)";

    }

    public void ChangeBGM(int clipNum)
    {
        // Sound Fade Out
        // Sound Fade In
    }

    public void PlayFade(int clipNum)
    {
        StartCoroutine(SoundFade(false));
        StartCoroutine(SoundFade(true, clipNum));
    }

    IEnumerator SoundFade(bool fadeIn, int clipNum = -1)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            yield return null;
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0.0f, currentTime / duration);
        }
    }
}