using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Manager<SoundManager>
{
    [SerializeField] private AudioMixer mixer;


    private AudioSource audioSource;     //Move to SoundtrackController; SoundtrackController should not be a singleton
    [SerializeField] private AudioClip introTrack;     //Move to SoundtrackController
    [SerializeField] private AudioClip ingameTrack;    //Move to SoundtrackController

    private void Start()
    {

        //Move to SoundtrackController
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;

        //Move to SoundtrackController
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    //Move to SoundtrackController
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        Debug.Log("GameState change in SoundManager");
        if (previousState == GameManager.GameState.PAUSED || currentState == GameManager.GameState.PAUSED)
            return;
        //Apply pause toogle effect on playing music by using mixer shootcut

        switch (currentState)
        {
            case (GameManager.GameState.RUNNING):
                PlayMusicTrack(ingameTrack);
                break;
            case (GameManager.GameState.INMENU):
                PlayMusicTrack(introTrack);
                break;
            case (GameManager.GameState.GAMEOVER):
                PlayMusicTrack(introTrack);
                break;
            default:
                break;
        }
    }

    //Move to SoundtrackController
    private void PlayMusicTrack(AudioClip track)
    {
        Debug.Log("Play track in SoundManager");
        audioSource.Stop();

        audioSource.clip = track;
        audioSource.PlayDelayed(1f);
    }

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
