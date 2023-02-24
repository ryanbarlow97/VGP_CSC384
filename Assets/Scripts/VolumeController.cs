using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
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
    }
}