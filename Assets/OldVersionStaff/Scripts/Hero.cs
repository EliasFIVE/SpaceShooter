using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; //Одиночка

    [Header("Set in Inspector")]
    public float speed = 30f;
    public float rollMult = -40;
    public float pitchMult = 20;
    public float energyMax = 100f;
    public float energyGeneration = 1f;
    public bool weaponOverheat = false;
    public float coolDownTime = 10f;
    public GameObject explosionPrefab;

    public Weapon[] weapons;
    //public Weapon[] support;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private float weaponOverheatTime;
    public float energy;

    private GameObject lastTriggerGo = null;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    private void Start()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assing second Hero.S !");
        }
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
        
        
        //    fireDelegate += TempFire;
    }
    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        WeaponOverheatChek();
        

        //Произвести выстрел из всех видов оружия вызовом fireDelegate
        if(Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            if (!weaponOverheat) { fireDelegate(); }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject.transform.root.gameObject;
        //print("Triggered:" + go.name);

        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            //print("Current shield level: " + shieldLevel);
            Die(go);
        } else if (go.tag == "PowerUp") {
            AbsorbPowerUp(go);
        } else
        {
            print(" Triggerd by non-Enemy: " + go.name);
        }

        if (go.tag == "Neutral" && shieldLevel == 0) { Die(this.gameObject); }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel++;
                break;
            default:
                if (pu.type == weapons[0].type)
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null) { w.SetType(pu.type); } else { energy += 100; }
                } else
                {
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)              
            {
                Die(this.gameObject);

            }
        }
    }
    void Die(GameObject target)
    {
        GameObject go = Instantiate<GameObject>(explosionPrefab);
        go.transform.position = this.gameObject.transform.position;
        Destroy(target.gameObject);
        if (target == this.gameObject) {
                float gameRestartDelay = Main.S.gameRestartDelay;
                Main.S.DelayedRestart(gameRestartDelay);
            }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++){
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach(Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
            weaponOverheatTime = 0f;
            energy = coolDownTime;
        }
    }
    void WeaponOverheatChek()
    {
        if (weaponOverheat)
        {
            if ((Time.time - weaponOverheatTime) >= coolDownTime)
            {
                weaponOverheat = false;
                energy = coolDownTime;
            }
        }
        else
        {
            if (energy < energyMax && !weaponOverheat) { energy += Time.deltaTime * energyGeneration; }
            if (energy <= 0) { weaponOverheat = true; weaponOverheatTime = Time.time; }
        }
    }
}
