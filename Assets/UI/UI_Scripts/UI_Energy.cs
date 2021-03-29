using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Energy : MonoBehaviour
{
    private Text text;
    private int energy;
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
            if (Hero.S.weaponOverheat)
            {
                text.color = Color.red;
                text.text = "overheat!";
            }
            else
            {
                text.color = Color.Lerp(Color.red, Color.green, Hero.S.energy / Hero.S.energyMax);
                text.text = ((int)Hero.S.energy).ToString();
            }
        }
    }
}
