using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    private TrailRenderer rendTrail;



    [SerializeField] private WeaponType type;
    private float shotTime;
    private float lifeTimeMax;
    private float lifeTime;

    private Rigidbody rigid;

    public void SetWeaponType(Weapon_SO weapon)
    {
        rend.material.color = weapon.projectileColor;
        rendTrail.material.color = weapon.projectileColor;
        lifeTimeMax = weapon.projectileLifeTime;
    }

    public void ResetProjectile(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        gameObject.transform.position = position;
        rigid.velocity = velocity;

        //Delay to trail enable necessary due to a viusal bug
        //most likely the time should be greater than the refresh rate of the camera renderer
        Invoke("TrailEnable", 0.1f);
    }

    private void TrailEnable()
    {
        rendTrail.enabled = true;
    }

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rendTrail = GetComponent<TrailRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        lifeTime = 0f;
    }

    void Update()
    {
        lifeTime +=Time.deltaTime;

        if (lifeTime > lifeTimeMax)
        {
            rendTrail.enabled = false;
            gameObject.SetActive(false);
        }
        if (!bndCheck.IsOnScreen)
        {
            rendTrail.enabled = false;
            gameObject.SetActive(false);
        }
    }


}
