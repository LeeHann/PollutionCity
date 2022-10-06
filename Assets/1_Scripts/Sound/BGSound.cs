using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    public static BGSound instance = null;

    public float duration = 1f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] commonClips;
    [SerializeField] AudioClip[] warningClips;
    
    List<AudioClip[]> clips = new List<AudioClip[]>();
    WaitForSeconds _1s = new WaitForSeconds(1f);

    private void Start() 
    {
        instance = this;
        DontDestroyOnLoad(this);
        this.gameObject.name += " (Singleton)";

        clips.Add(commonClips);
        clips.Add(warningClips);
        StartCoroutine(NextTrack());
    }

    IEnumerator NextTrack()
    {   
        while (true)
        {
            yield return _1s;
            if (!audioSource.isPlaying) {
                int next = Random.Range(0, clips[(int)MetaAI.State].Length);
                audioSource.clip = clips[(int)MetaAI.State][next];
                audioSource.Play();
            }
        }
    }
    public void ChangeBGM()
    {
        StartCoroutine(SoundFade());
    }

    IEnumerator SoundFade()
    {
        float currentTime = 0f;
        float start = audioSource.volume;
        
        // fade out
        while (currentTime < duration)
        {
            yield return null;
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0.0f, currentTime / duration);
        }

        audioSource.clip = clips[(int)MetaAI.State][0];

        // fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            yield return null;
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0.0f, start, currentTime / duration);
        }
    }
}