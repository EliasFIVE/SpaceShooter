using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitToMenuButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(HandleResumeClicked);
        optionsButton.onClick.AddListener(HandleOptionsClicked);
        exitToMenuButton.onClick.AddListener(HandleExitToMenuClicked);
    }

    private void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    private void HandleOptionsClicked()
    {
        UIManager.Instance.ToggleOptionsMenu();
    }

    private void HandleExitToMenuClicked()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
