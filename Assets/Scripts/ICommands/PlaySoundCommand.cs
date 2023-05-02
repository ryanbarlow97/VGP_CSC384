using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaySoundCommand : ICommand
{
    private AudioClip clip;
    private MonoBehaviour monoBehaviour;
    private AudioSource audioSource;
    private bool loop;


    public PlaySoundCommand(MonoBehaviour monoBehaviour, AudioClip clip, bool loop = false)
    {
        this.monoBehaviour = monoBehaviour;
        this.clip = clip;
        this.loop = loop;
    }

    public void Execute()
    {
        GameObject audioSourceObject = new GameObject("AudioSourceObject");
        audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = 1.0f;
        audioSource.loop = loop;
        audioSource.Play();
        if (!loop)
        {
            monoBehaviour.StartCoroutine(DestroyAfterPlaying());
        }
    }

    private IEnumerator DestroyAfterPlaying()
    {
        yield return new WaitForSeconds(clip.length);

        if (audioSource != null)
        {
            Object.Destroy(audioSource.gameObject);
        }
    }
}
