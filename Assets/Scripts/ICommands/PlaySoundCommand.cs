using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaySoundCommand : ICommand
{
    private AudioClip clip;
    private MonoBehaviour monoBehaviour;
    private AudioSource audioSource;
    private GameObject audioSourceObject;
    private bool loop;


    public PlaySoundCommand(MonoBehaviour monoBehaviour, AudioClip clip, bool loop = false)
    {
        this.monoBehaviour = monoBehaviour;
        this.clip = clip;
        this.loop = loop;
    }

    public void Execute()
    {
        audioSourceObject = new GameObject("AudioSourceObject");
        audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = 1.0f;
        audioSource.loop = loop;
        audioSource.Play();
        if (!loop)
        {
            Object.Destroy(audioSourceObject, clip.length+0.2f);
        }
    }
}
