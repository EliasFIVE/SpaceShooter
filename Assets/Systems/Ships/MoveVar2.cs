using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class MoveVar2 : MoveShip
{
    [Header("Set in Inspector")]
    [SerializeField] private float lifeTime = 30;

    private Vector3[] points;


    protected override void Start()
    {
        base.Start();

        points = new Vector3[3];
        points[0] = initialPosition;

        float xMin = -borders.CamWidth + bound.radius;
        float xMax = borders.CamWidth - bound.radius;
        Vector3 v;
        //Randomly select the midpoint of the lower border of the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -borders.CamHeight * Random.Range(2.75f, 2);
        points[1] = v;

        //Randomly select an endpoint above the top border of the screen
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;
    }

    public override void Move(float speed, float deltaTime)
    {
        float u = (Time.time - birthTime) / lifeTime;
        if (u > 1)
        {
            Vector3 tempPos = pos;
            tempPos.y -= speed * deltaTime;
            pos = tempPos;
            return;
        }

        //Bezier curves based on u values between 0 and 1
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }
}
