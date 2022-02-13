using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioClip musicSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Pause()
    {
        musicAudioSource.Pause();
    }
    public void Play()
    {
        musicAudioSource.Play();
    }
    public void StartMusic()
    {
        float volume = PlayerPrefs.GetFloat("masterVolume", AudioListener.volume);
        musicAudioSource.volume = volume / 2;
        musicAudioSource.Stop();
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicSound;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
}
