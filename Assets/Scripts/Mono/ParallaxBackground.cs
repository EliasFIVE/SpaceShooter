using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject[] panels;
    public float scrollSpeed = -30f; //Speed of background movement
    public float motionMult = 0.25f; // value of reaction to player move

    private float panelHt;// hieght of panel
    private float depth; //pos.z of panel
    private Transform player; //player ship transform

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;

        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;

        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);
    }

    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);

        if (player != null)
            tX = -player.position.x * motionMult;

        //Change panel
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
