using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer audioMixer;
    //[SerializeField] Slider musicSlider;
    //[SerializeField] Slider sfxSlider;
    private bool isMusicMuted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float musicVolume;
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        //musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });

        float sfxVolume;
        audioMixer.GetFloat("SFXVolume", out sfxVolume);
        //sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    public void ChangeMusicVolume()
    {
        //audioMixer.SetFloat("SFXVolume", sfxSlider.value); //not using sliders anymore 
        if (isMusicMuted)
        {
            
            audioMixer.SetFloat("MasterVolume", 0); 
            isMusicMuted = false; 
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", -80); 
            isMusicMuted = true; 
        }
    }

    public void ChangeSFXVolume()
    {
        
    }
}
