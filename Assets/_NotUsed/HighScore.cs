using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int totalKills = 0;
    static public int totalSoots = 0;
    static public int score = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "Hight Score: " + score;

        if (score > PlayerPrefs.GetInt("HightScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
