using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaySoundCommand : ICommand
{
    private AudioSource audioSource;
    private AudioClip sound;

    public PlaySoundCommand(AudioSource audioSource, AudioClip sound)
    {
        this.audioSource = audioSource;
        this.sound = sound;
    }

    public void Execute()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}