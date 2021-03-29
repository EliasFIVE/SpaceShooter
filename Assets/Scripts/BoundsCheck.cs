using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Предотвращает выход игрового объекта за границы экрана.
/// ВАЖНО! Работает только с ортографической камерой в [0,0,0]
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeights;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    private void Awake()
    {
        camHeights = Camera.main.orthographicSize;
        camWidth = camHeights * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        //isOnScreen = true;
        offRight = offLeft = offDown = offUp = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeights - radius)
        {
            pos.y = camHeights - radius;
            offUp = true;
        }
        if (pos.y < -camHeights +radius)
        {
            pos.y = -camHeights + radius;
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

    //Рисует границы в панели Scene 
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeights * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

}
