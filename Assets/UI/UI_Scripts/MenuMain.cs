using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
