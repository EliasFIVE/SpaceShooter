using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    [SerializeField] private Camera dummyCamera;
    [SerializeField] private GameObject bootMenu;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameOverMenu gameOverMenu;
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("UI Space pressed");
            GameManager.Instance.GoToMainMenu();
        }
    }

    public void StartNewGame()
    {
        GameManager.Instance.StartNewGame();
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        dummyCamera.gameObject.SetActive(currentState == GameManager.GameState.PREGAME);
        bootMenu.SetActive(currentState == GameManager.GameState.PREGAME);
        mainMenu.gameObject.SetActive(currentState == GameManager.GameState.INMENU);
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        gameOverMenu.gameObject.SetActive(currentState == GameManager.GameState.GAMEOVER);
        //bool showUnitFrame = currentState == GameManager.GameState.RUNNING || currentState == GameManager.GameState.PAUSED;
        //unitFrame.SetActive(showUnitFrame);
    }
}
