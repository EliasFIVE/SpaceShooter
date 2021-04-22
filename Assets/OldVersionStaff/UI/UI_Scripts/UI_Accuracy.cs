using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Accuracy : MonoBehaviour
{
    private Text text;
    private int accuracy;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        accuracy = (int)(Main.accuracy*100);
        text.color = Color.Lerp(Color.red, Color.green, Main.accuracy);
        text.text = accuracy.ToString() + "%";
    }
}
