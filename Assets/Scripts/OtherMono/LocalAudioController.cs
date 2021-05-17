using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Local version of sound manager to control sound fx of a single object;
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class LocalAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource.outputAudioMixerGroup == null)
            Debug.LogWarningFormat("Audio output not set to {0} object", gameObject.name);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    [System.Obsolete]
    public void PlayClip(AudioClip clip, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.RandomRange(minPitch, maxPitch);
        PlayClip(clip);
    }
}
