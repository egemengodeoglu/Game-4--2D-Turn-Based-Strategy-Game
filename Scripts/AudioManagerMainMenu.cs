using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    public AudioClip menuSound;
    public Slider masterVolumeSlider;
    public Slider effectVolumeSlider;
    AudioSource source;
    private void Awake() {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = menuSound;
        source.loop = true;
        source.Play();
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        effectVolumeSlider.value = PlayerPrefs.GetFloat("effectVolume");
    }
    public void EffectVolume(System.Single vol){
        PlayerPrefs.SetFloat("effectVolume", vol);
    }

    public void MasterVol(System.Single vol){
        PlayerPrefs.SetFloat("masterVolume", vol);
        source.volume = vol;
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
