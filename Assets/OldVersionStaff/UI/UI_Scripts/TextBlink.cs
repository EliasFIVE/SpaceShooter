using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Color color1;
    public Color color2;
    public float freq = 0.1f;

    private Text text;
    private float u = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        u = Mathf.Abs(Mathf.Sin(freq * Time.time));
        text.color = Color.Lerp(color1, color2, u);
    }
}
