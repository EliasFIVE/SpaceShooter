using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    private TrailRenderer rendTrail;


    [Header("Set Dynamically")]
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;
    private float shotTime;
    private float lifeTimeMax;
    private float lifeTime = 0f;
    private Color color;

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    private void Awake()
    {
        
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rendTrail = GetComponent<TrailRenderer>();
        rigid = GetComponent<Rigidbody>();
        shotTime = Time.time;
    }



    // Update is called once per frame
    void Update()
    {
        lifeTime = Time.time - shotTime;
//        color = rend.material.color;
//        color.a = 100f;
//        color.a = 255f + lifeTime / lifeTimeMax * (100f - 255f);
//        rend.material.color = color;

        if (lifeTime > lifeTimeMax)
        {
            Destroy(gameObject);
        }
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }

    ///<summary>
    ///Изменяет скрыто поле _type и устанавливает цвет этого снаряда, как определено в WeaponDefinition
    /// </summary>
    /// <param name="eType"> Тип WeaponType используемого оружия
    /// </param>
    public void SetType (WeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
        rendTrail.material.color = def.projectileColor;
        lifeTimeMax = def.projectileLifeTime;
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    GameObject otherGO = collision.gameObject;
    //    if (otherGO.tag == "Enemy")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
