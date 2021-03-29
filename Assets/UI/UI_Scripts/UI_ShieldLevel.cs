using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShieldLevel : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Hero.S != null)
        {
            text.text = Hero.S.shieldLevel.ToString();
        }
    }
}
