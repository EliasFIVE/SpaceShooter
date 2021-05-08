using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVar1 : MoveShip
{
    [Header("Set in Inspector")]
    [SerializeField] private float waveFrequency = 2;
    [SerializeField] private float waveWidth = 4;
    [SerializeField] private float waveRotY = 45;

    public override void Move(float speed, float deltaTime)
    {
        Vector3 tempPos = pos;
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = initialPosition.x + waveWidth * sin;
        pos = tempPos;

        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        //tempPos = pos;
        tempPos.y -= speed * deltaTime;
        pos = tempPos;
    }
}
