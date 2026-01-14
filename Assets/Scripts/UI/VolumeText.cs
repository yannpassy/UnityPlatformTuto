using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; //  Sound: or Music:
    private Text txt;

    private void Awake() 
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        updateVolume();
    }

    private void updateVolume()
    {
        float volume = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text= textIntro + volume.ToString();
    }
}
