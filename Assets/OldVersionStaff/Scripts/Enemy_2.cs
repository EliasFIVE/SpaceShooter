using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Set in Inspector: Enemy_2")]
    //Насколько ярко выражен синусоидальный характер
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")]
    //Enemy_2 использует линейную интерполяцию между двумя точками, изменяя результат по синусоиде
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p01;
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        //Случайная точка на левой границе экрана
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(-bndCheck.camHeights, bndCheck.camHeights);

        //Случайная точка на правой границе экрана
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeights, bndCheck.camHeights);

        //Случайно поменять начальную и конечную точку сторонами экрана
        if (Random.value > 0.5f)
        {
            p0.x *= -1;
            p1.x *= -1;
        }
        //Чтобы стартовая позиция была в верхней половине экрана, а конечная точка в нижней
        if (p0.y<0) { p0.y *= -1; }
        if (p1.y>0) { p1.y *= -1; }

        pos = p0;
        birthTime = Time.time;
        
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;
        if (u > 1) //объект живет дольше заданного lifeTime
        {
            Destroy(this.gameObject);
            return;
        }

        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        p01 = (1 - u) * p0 + u * p1;
        pos = p01;
        //base.Move();
    }
}
