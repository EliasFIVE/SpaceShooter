using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Manager<SoundManager>
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerSnapshot initSnapshot;
    [SerializeField] private AudioMixerSnapshot pauseSnapshot;

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

    public void SetInitSnapshot()
    {
        initSnapshot.TransitionTo(0.5f);
    }

    public void SetPauseSnapshot()
    {
        pauseSnapshot.TransitionTo(0.5f);
    }

    public void ToglePauseCutLP(bool pause)
    {
        
        if (pause)
        {
            pauseSnapshot.TransitionTo(0.001f); //when timescale == 0 this will stop transition alse, so need to set so low time to manage this
            Debug.Log("LP CUT ON");
        }
        else
        {
            initSnapshot.TransitionTo(0.001f);
            Debug.Log("LP CUT OFF");
        }
            
    }
}
