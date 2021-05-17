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
    [SerializeField] private InGameUIManager inGameUI;
    [SerializeField] private OptionsMenu optionsMenu;
 
    [SerializeField] private ExciterController textExciter;

    public InGameUIManager InGameUI
    {
        get { return inGameUI; }
    }
    private void Start()
    {
        if(GameManager.Instance !=null)
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("UI Space pressed");
                GameManager.Instance.GoToMainMenu();
            }
        }
    }

    public void StartNewGame()
    {
        GameManager.Instance.StartNewGame();
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        Debug.LogFormat("Game state {0}", currentState.ToString());

        dummyCamera.gameObject.SetActive(currentState == GameManager.GameState.PREGAME);
        bootMenu.SetActive(currentState == GameManager.GameState.PREGAME);

        mainMenu.gameObject.SetActive(currentState == GameManager.GameState.INMENU);

        inGameUI.gameObject.SetActive(currentState == GameManager.GameState.RUNNING);

        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);

        gameOverMenu.gameObject.SetActive(currentState == GameManager.GameState.GAMEOVER);
    }

    //Exciters setup
    public void ShowTextExciter(string text)
    {
        //Debug.Log("Show exciter");

        textExciter.gameObject.SetActive(true);
        textExciter.ShowExciter(text);

    }

    public void ToggleOptionsMenu()
    {
        if (optionsMenu.gameObject.activeInHierarchy)
        {
            optionsMenu.gameObject.SetActive(false);
        }
        else
        {
            optionsMenu.gameObject.SetActive(true);
        }
    }
}
