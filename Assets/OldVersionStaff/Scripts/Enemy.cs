using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float speed = 10f; //скорость в м/с
    public float fireRate = 0.3f; //секунд между выстрелами
    public float health = 10;
    public int score = 100; //очки за уничтожение
    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 1f;
    public GameObject explosionPrefab;
    public AudioClip hitFX;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestrucion = false;

    protected BoundsCheck bndCheck;
    public AudioSource audio;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        audio.playOnAwake = false;

        bndCheck = this.GetComponent<BoundsCheck>();
        
        //Get materials and colors of this GO and childs
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i<materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime) { UnShowDamage(); }

        if (bndCheck !=null && bndCheck.offDown)
        {
            Destroy(gameObject);           
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                //Если вражеский корабль за границей экрана - не наносить ему повреждений
                if (!bndCheck.IsOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }

                
                ShowDamage();
                Main.enemyHit++;
                audio.PlayOneShot(hitFX, 0.5F);

                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    if (!notifiedOfDestrucion) { Main.S.ShipDestroyed(this); }
                    notifiedOfDestrucion = true;
                    Die() ;                    
                }
                Destroy(otherGO);
                break;

            case "Neutral":
                Die();
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }

    void Die()
    {
        GameObject go = Instantiate<GameObject>(explosionPrefab);
        go.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i=0; i<materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
