using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

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
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });

        float sfxVolume;
        audioMixer.GetFloat("SFXVolume", out sfxVolume);
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);
    }
}
