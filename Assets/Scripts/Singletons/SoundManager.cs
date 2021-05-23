using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Manager<SoundManager>
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float volume)
    {
        //Debug.Log("MasterVolumeChange");
        mixer.SetFloat("volumeMaster", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("volumeMusic", volume);
    }

    public void SetFXVolume(float volume)
    {
        mixer.SetFloat("volumeFX", volume);
    }
}
