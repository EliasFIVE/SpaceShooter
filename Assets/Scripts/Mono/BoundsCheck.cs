using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controls object locatetion in screen bounds
/// Works fine only with orthograohic camera located at [0,0,0]
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; //set True if you want this object to stay in screen bounds

    [Header("Set Dynamically")]
    [SerializeField] private bool isOnScreen = true;
    public bool offRight, offLeft, offUp, offDown; //Make Private with get only
    public bool IsOnScreen
    {
        get { return isOnScreen; }
    }

    private ScreenBorders borders;

    private void Start()
    {
        borders = ScreenBorders.Instance;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        offRight = offLeft = offDown = offUp = false;

        if (pos.x > borders.CamWidth - radius)
        {
            pos.x = borders.CamWidth - radius;
            offRight = true;
        } else if (pos.x < -borders.CamWidth + radius)
        {
            pos.x = -borders.CamWidth + radius;
            offLeft = true;
        }

        if (pos.y > borders.CamHeight - radius)
        {
            pos.y = borders.CamHeight - radius;
            offUp = true;
        } else if (pos.y < -borders.CamHeight + radius)
        {
            pos.y = -borders.CamHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }        
    }
}
