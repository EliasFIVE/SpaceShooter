using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(HandleStartClicked);
        optionsButton.onClick.AddListener(HandleOptionsClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    private void HandleOptionsClicked()
    {
        throw new NotImplementedException();
    }

    private void HandleStartClicked()
    {
        GameManager.Instance.StartNewGame();
    }
}
