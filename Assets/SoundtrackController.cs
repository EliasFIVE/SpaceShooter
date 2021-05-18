using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour
{
    private AudioSource audioSource;     
    [SerializeField] private AudioClip introTrack;
    [SerializeField] private AudioClip ingameTrack;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;

        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

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

    private void PlayMusicTrack(AudioClip track)
    {
        Debug.Log("Play track in SoundManager");
        audioSource.Stop();

        audioSource.clip = track;
        audioSource.PlayDelayed(1f);
    }

}
