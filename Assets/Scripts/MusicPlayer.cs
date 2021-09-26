using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    AudioSource audioSource;
    AudioClip currentSong;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            currentSong = audioSource.clip;
        }
    }
    public void PlayMusic(AudioClip music)
    {
        Debug.Log("playing some pogful music");
        if(music != currentSong)
        {
            currentSong = music;
            audioSource.clip = music;
            audioSource.Play();
            audioSource.volume = 1;
        }
    }
    public void PlayMusic(AudioClip music, float transitionSpeed)
    {
        if(music != currentSong)
        {
            StartCoroutine(TransitionSmoothly(music, transitionSpeed));
        }
    }
    IEnumerator TransitionSmoothly(AudioClip music, float transitionSpeed)
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -= transitionSpeed * Time.deltaTime;
        }
        currentSong = music;
        audioSource.clip = music;
        audioSource.Play();
        audioSource.volume = 1;
        yield break;
    }
}
