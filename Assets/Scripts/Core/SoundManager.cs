using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake() 
    {
        
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); 

        if(instance == null)
        {
            instance = this;
            // don't destroy SoundManager when the scene is changed
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            // destroy duplicate SoundManager
            Destroy(gameObject);
        }
        
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    private void ChangeSourceVolume(AudioSource _source,string _volumeName, float _change)
    {
        float currentVolume = PlayerPrefs.GetFloat(_volumeName, 1) + _change;

        if(currentVolume >1)
            currentVolume = 0;
        else if(currentVolume <0)
            currentVolume = 1;

        _source.volume = currentVolume;
        PlayerPrefs.SetFloat(_volumeName, currentVolume);

    }
    public void ChangeSoundVolume(float _change)
    {
        // float currentVolume = PlayerPrefs.GetFloat("soundVolume", 1) + _change;

        // if(currentVolume >1)
        //     currentVolume = 0;
        // else if(currentVolume <0)
        //     currentVolume = 1;

        // soundSource.volume = currentVolume;
        // PlayerPrefs.SetFloat("soundVolume", currentVolume); 

        ChangeSourceVolume(soundSource, "soundVolume", _change);
    }

    public void ChangeMusicVolume(float _change)
    {
    //     float currentVolume = PlayerPrefs.GetFloat("musicVolume", 1)  + _change;

    //     if(currentVolume >1)
    //         currentVolume = 0;
    //     else if(currentVolume <0)
    //         currentVolume = 1;

    //     musicSource.volume = currentVolume;
    //     PlayerPrefs.SetFloat("musicVolume", currentVolume); 
        ChangeSourceVolume(musicSource, "musicVolume", _change);
    }
}
