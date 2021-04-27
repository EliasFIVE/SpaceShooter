using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon: MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;
    
    [Header("Set In Inspector")]
    public float shootDelayTime = 0;

    [Header("Set Dynamically")]
    [SerializeField] private WeaponType _type = WeaponType.none;
    public WeaponDefinition weaponDefinition;

    private GameObject collar; //Dummy object to show visual identification of current set weapon and to set initial projectile position
    private Renderer collarRend;

    private AudioSource audio;
    private AudioClip shootFX;

    private float lastShotTime; //Время последнего выстрела
    private void Start()
    {
        collar = transform.Find("Collar").gameObject; //Not best descision: to find object by string name. Need to think of alt way. But don't want to set it manualy in inspector
        collarRend = collar.GetComponent<Renderer>();
        
        audio = GetComponent<AudioSource>();
        //Вызвать SetType(), чтобы заменить тип оружия по умолчанию
        //WeaponType.none
        SetType(_type);
        //Динамически создать точку привязки для всех снарядов
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        //Найти fireDelegate в корневом игровом объекте
        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += FireDelayed;
        }
    }

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType (WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        } else
        {
            this.gameObject.SetActive(true);
        }
        weaponDefinition = Main.GetWeaponDefinition(_type);
        collarRend.material.color = weaponDefinition.color;
        shootFX = weaponDefinition.shootFX;
        lastShotTime = 0;
    }
    
    public void FireDelayed()
    {
        Invoke("Fire", shootDelayTime);
    }


    ///Transfer to weapon definition SO???
    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time - lastShotTime < weaponDefinition.delayBetweenShots) return;
        Projectile p;
        Vector3 vel = Vector3.up * weaponDefinition.velocity;
        if (transform.up.y < 0) vel.y = -vel.y;

        audio.PlayOneShot(shootFX, 0.5F);

        switch (type)
        {
            case WeaponType.blaster:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                break;

            case WeaponType.spread:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                break;

            case WeaponType.phaser:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                break;
        }


    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(weaponDefinition.projectilePrefab);
        if(transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        } else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.time;

        Main.projectilesLunched++; //call For global stats


        Hero.S.energy -= Main.GetWeaponDefinition(p.type).projectileEnergyCost; // call For player stats


        return (p);
    }

}

