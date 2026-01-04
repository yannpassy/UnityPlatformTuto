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

        // init currentVolume in PlayerPrefs
        PlayerPrefs.SetFloat("soundVolume", 1); 
        PlayerPrefs.SetFloat("musicVolume", 1); 

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

    public void ChangeSoundVolume(float _change)
    {
        float currentVolume = PlayerPrefs.GetFloat("soundVolume") + _change;

        if(currentVolume >1)
            currentVolume = 0;
        else if(currentVolume <0)
            currentVolume = 1;

        soundSource.volume = currentVolume;
        PlayerPrefs.SetFloat("soundVolume", currentVolume); 
    }

    public void ChangeMusicVolume(float _change)
    {
        float currentVolume = PlayerPrefs.GetFloat("musicVolume")  + _change;

        if(currentVolume >1)
            currentVolume = 0;
        else if(currentVolume <0)
            currentVolume = 1;

        musicSource.volume = currentVolume;
        PlayerPrefs.SetFloat("musicVolume", currentVolume); 
    }
}
