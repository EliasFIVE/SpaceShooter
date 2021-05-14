using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        retryButton.onClick.AddListener(HandleRetryClicked);
        mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
    }

    private void HandleMainMenuClicked()
    {
        GameManager.Instance.GoToMainMenu();
    }

    private void HandleRetryClicked()
    {
        GameManager.Instance.StartNewGame(); 
    }

}
