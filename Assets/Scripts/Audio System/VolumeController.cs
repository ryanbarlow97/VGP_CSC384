using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    private void Start()
    {
        // Set the position of the slider to the loaded volume value
        slider.value = VolumeManager.GetSavedVolumePreference();
    }

    public void SetVolume(float volume)
    {
        float db;

        if (volume == 0f) {
            // Mute the sound
            db = -80f;
        } else {
            // Convert linear volume to decibels using logarithmic scale
            db = Mathf.Log10(volume) * 20f;
        }

        audioMixer.SetFloat("MasterVolume", db);

        // Store the volume value in PlayerPrefs
        VolumeManager.volume = volume;
        VolumeManager.SaveVolumePreference();
    }
}
