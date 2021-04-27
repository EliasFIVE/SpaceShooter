using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Создается за верхней границей, выбирает случайную точку на экране и перемещается к ней.
/// Добравшись до места, выбирает другую случайную точку и продолжает двигаться, пока игрок не уничтожит его.
/// </summary>
/// 
[System.Serializable]
public class Part
{
    //Значение этих трех полей должны определяться в инспекторе
    public string name; //Имя этой части
    public float health; //
    public string[] protectedBy; //Другие части, защищающие эту

    [HideInInspector]
    public GameObject go;//Игровой объект этой части
    [HideInInspector]
    public Material mat;
}
public class Enemy_4 : Enemy
{
    [Header("Set in Inspector")]
    public Part[] parts; //Массив частей, составляющих корабль

    private Vector3 p0, p1; //точки для интерполяции
    private float timeStart;
    private float duration = 4; //Продолжительность перемещения

    private void Start()
    {
        //Начальная позиция уже выбрана в Main.SpawnEnemy();
        p0 = p1 = pos;
        InitMovement();

        Transform t;
        foreach(Part prt in parts)
        {
            t = transform.Find(prt.name);
            if(t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
            }
        }
    }

    void InitMovement()
    {
        p0 = p1;
        //float widMinRad = bndCheck.camWidth - bndCheck.radius;
        //float hgtMinRad = bndCheck.camHeights - bndCheck.radius;
        //p1.x = Random.Range(-widMinRad, widMinRad);
        //p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        // Сбросить время
        timeStart = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2);
        pos = (1 - u) * p0 + u * p1;
    }

    Part FindPart(string n)
    {
        foreach(Part prt in parts)
        {
            if (prt.name == n)
            {
                return (prt);
            }
        }
        return (null);
    }

    Part FindPart (GameObject go)
    {
        foreach(Part prt in parts)
        {
            if(prt.go == go)
            {
                return (prt);
            }
        }
        return (null);
    }

    //Возвращают true если данная часть уничтожена
    bool Destroyed(GameObject go)
    {
        return (Destroyed(FindPart(go)));
    }
    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }
    bool Destroyed(Part prt)
    {
        if (prt == null)
        {
            return (true);
        }
        return (prt.health <= 0);
    }

    //Окрашивает в красный только одну часть корабля
    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        damageDoneTime = Time.time + showDamageDuration;
        showingDamage = true;

    }

    //Переопределяет метод
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                
                Projectile p = other.GetComponent<Projectile>();
                if (!bndCheck.IsOnScreen)
                {
                    Destroy(other);
                    break;
                }

                Main.enemyHit++;
                audio.PlayOneShot(hitFX, 0.5F);
                //Поразить корабль
                GameObject goHit = collision.contacts[0].thisCollider.gameObject;
                Part prtHit = FindPart(goHit);
                if (prtHit == null)
                {
                    goHit = collision.contacts[0].otherCollider.gameObject;
                    prtHit = FindPart(goHit);
                }
                //Проверить защищена ли эта часть корабля
                if (prtHit.protectedBy != null)
                {
                    foreach(string s in prtHit.protectedBy)
                    {
                        if (!Destroyed(s))
                        {
                            Destroy(other); //уничтожить снаряд
                            return;
                        }
                    }
                }
                //Часть не защищена - нанести ей повреждения
                prtHit.health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowLocalizedDamage(prtHit.mat);
                if (prtHit.health <= 0)
                {
                    prtHit.go.SetActive(false);
                }

                //Проверить, был ли корабль полностью разрушен
                bool allDestroyed = true;
                foreach(Part prt in parts)
                {
                    if (!Destroyed(prt))
                    {
                        allDestroyed = false;
                        break;
                    }
                }

                if (allDestroyed)
                {
                    Main.S.ShipDestroyed(this);
                    Destroy(this.gameObject);
                    Main.enemyKilled++;
                }
                Destroy(other);
                break;


        }
    }
}

    

