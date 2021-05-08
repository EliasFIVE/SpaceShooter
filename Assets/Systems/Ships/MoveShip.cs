using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class MoveShip : MonoBehaviour
{
    protected float birthTime;
    protected Vector3 initialPosition;
    protected float speed;

    protected BoundsCheck bound;
    protected ScreenBorders borders;


    protected virtual void Start()
    {
        bound = gameObject.GetComponent<BoundsCheck>();
        borders = ScreenBorders.Instance;
        speed = gameObject.GetComponent<ShipStats>().Speed;

        initialPosition = pos;
        birthTime = Time.time;
    }

    public Vector3 pos
    {
        get
        {
            return (gameObject.transform.position);
        }
        set
        {
            gameObject.transform.position = value;
        }
    }

    public virtual void Move(float speed, float deltaTime)
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * deltaTime;
        pos = tempPos;
    }

    private void Update()
    {
        Move(speed, Time.deltaTime);
    }
}
