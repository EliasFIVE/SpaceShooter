using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponType : MonoBehaviour
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
        if (Hero.S != null)
        {
            text.color = Hero.S.weapons[0].collar.GetComponent<Renderer>().material.color;
            text.text = Hero.S.weapons[0].type.ToString();
        }

    }
}
