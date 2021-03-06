using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    private TrailRenderer rendTrail;


    [SerializeField] private WeaponType type; //to check weapon type in inspector
    private int damagePower;
    //private float shotTime;
    private float lifeTimeMax;
    private float lifeTime;

    private Rigidbody rigid;
    
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rendTrail = GetComponent<TrailRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    #region Reset setup
    public void ResetProjectile(Vector3 position, Quaternion rotation, float speed)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        rigid.velocity = gameObject.transform.up * speed;

        //Delay to trail enable necessary due to a viusal bug
        //most likely the time should be greater than the refresh rate of the camera renderer
        Invoke("TrailEnable", 0.1f);
    }

    private void TrailEnable() //Used in ResetProjectile by Invoke method
    {
        rendTrail.enabled = true;
    }

    private void OnEnable()
    {
        lifeTime = 0f;
    }
    #endregion

    void Update()
    {
        lifeTime +=Time.deltaTime;

        if (lifeTime > lifeTimeMax)
            DeactivateProjectile();
    }

    public void SetWeaponType(Weapon_SO weapon)
    {
        rend.material.color = weapon.projectileColor;
        rendTrail.material.color = weapon.projectileColor;
        lifeTimeMax = weapon.projectileLifeTime;
        type = weapon.weaponType;
        damagePower = weapon.damageOnHit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoundsCheck>().IsOnScreen)         // if target object is out of screen bounds - not damage it
        {
            var damagables = collision.gameObject.GetComponents<IDamagable>();
            foreach (IDamagable damagable in damagables)
            {
                damagable.TakeDamage(damagePower);
            }
        }

        DeactivateProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShieldController>() != null)
        {
            ShieldController shield = other.gameObject.GetComponent<ShieldController>();
            if (shield.ShieldLevel == 0)
            {
                return;
            }
            else
            {
                shield.AbsorbDamage(damagePower);
                DeactivateProjectile();
            }
        }
    }

    private void DeactivateProjectile()
    {
        rendTrail.enabled = false;
        gameObject.SetActive(false);
    }
}
