using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource backgroundAudioSource, effectAudioSource;

    public Slider backgroundVolumeSlider;
    public Slider effectVolumeSlider;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            _instance = FindObjectOfType<AudioManager>();
            return _instance;
        }
    }


    private void Awake() 
    {
        backgroundAudioSource = GetComponents<AudioSource>()[0];
        SetBackgroundVolume(PlayerPrefs.GetFloat("masterVolume"));
        backgroundVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

        effectAudioSource = GetComponents<AudioSource>()[1];
        effectVolumeSlider.value = PlayerPrefs.GetFloat("effectVolume");
    }

    public void PlaySound(AudioClip clip)
    {
        effectAudioSource.PlayOneShot(clip);
    }
    
    public void SetEffectVolume(System.Single vol)
    {
        effectAudioSource.volume = vol;
        PlayerPrefs.SetFloat("effectVolume", vol);
    }

    public void SetBackgroundVolume(System.Single vol){
        backgroundAudioSource.volume = vol;
        PlayerPrefs.SetFloat("masterVolume", vol);
    }


}
