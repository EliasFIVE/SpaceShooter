using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : Singleton<ScreenBorders>
{
    private float camWidth; //Make Private
    private float camHeights; //Make Private

    public float CamWidth
    {
        get { return camWidth; }
    }

    public float CamHeight
    {
        get { return camHeights; }
    }

    void Start()
    {
        camHeights = Camera.main.orthographicSize;
        camWidth = camHeights * Camera.main.aspect;
    }

    #region For debugging

    //Draw Bounds in Scene view
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeights * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
    #endregion
}
