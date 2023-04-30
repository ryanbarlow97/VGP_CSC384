using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioMixer audioMixer;

    void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            // If not, set the instance to this object
            instance = this;

            // Set DontDestroyOnLoad so that this object is not destroyed when a new scene is loaded
            DontDestroyOnLoad(this.gameObject);

            float db;

                if (VolumeManager.GetSavedVolumePreference() == 0f) {
                    // Mute the sound
                    db = -80f;
                } else {
                    // Convert linear volume to decibels using logarithmic scale
                    db = Mathf.Log10(VolumeManager.GetSavedVolumePreference()) * 20f;
                }
            // Set the volume of the audio source to the saved volume value

            audioMixer.SetFloat("MasterVolume", db);

            // Play the audio source
            audioSource.Play();
        }
        else
        {
            // If an instance of AudioManager already exists, destroy this object
            Destroy(this.gameObject);
        }
    }
}
