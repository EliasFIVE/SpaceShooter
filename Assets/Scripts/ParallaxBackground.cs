using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject[] panels;
    public float scrollSpeed = -30f;
    public float motionMult = 0.25f; // степень реакции на перемещение кораблья игрока

    private float panelHt;// высота панели
    private float depth; //pos.z панели
    private Transform poi; //Корабль игрока

    // Start is called before the first frame update
    void Start()
    {
        poi = FindObjectOfType<PlayerController>().gameObject.transform;

        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;

        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);

    }

    // Update is called once per frame
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);

        if (poi != null)
        {
            tX = -poi.position.x * motionMult;
        }
        //Сменить панель
        panels[0].transform.position = new Vector3(tX, tY, depth);
        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHt, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHt, depth);
        }
    }
}
