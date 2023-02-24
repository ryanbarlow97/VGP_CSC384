using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    private const string VOLUME_KEY = "Volume";

    public AudioMixer audioMixer;

    private void Start()
    {
        // Load the last volume value from PlayerPrefs
        float volume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        SetVolume(volume);
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
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
}
