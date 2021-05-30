using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private ShipController ship;

    private GameObject collar; //Dummy object to show visual identification of current set weapon and to set initial projectile position
    private Renderer collarRend;

    [Header("Set In Inspector")]
    public float shootDelayTime = 0; //Set in inspector different time for guns in single ship to make them shoot one by one

    [Header("Set Dynamically")]
    [SerializeField] private WeaponType weaponType = WeaponType.none;
    [SerializeField] private Weapon_SO weaponDefinition;
/*    private float lastShotTime; //Time from last shot*/
    public WeaponType WeaponType
    {
        get { return (weaponType); }
        set { SetType(value); }
    }

    /// <summary>
    /// Apply weapon type for weapon. If weapon type is none, deactivates weapon gameonject
    /// </summary>
    /// <param name="type"></param>
    public void SetType(WeaponType type)
    {
        weaponType = type;
        if (type == WeaponType.none)
        {
            weaponDefinition = null;
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        weaponDefinition = WeaponManager.Instance.GetWeaponDefinition(type);
        collarRend.material.color = weaponDefinition.color;
/*        lastShotTime = 0;*/
    }

    #region Initial setup
    private void Awake()
    {
        collar = transform.Find("Collar").gameObject; //Not best descision: to find object by string name. Need to think of alt way. But don't want to set it manualy in inspector
        collarRend = collar.GetComponent<Renderer>();

        //Find fireDelegate in root game object
        // Attention!!!! If you create parent object to ship itself, it will break WeaponController
        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<ShipController>() != null)
        {
            rootGO.GetComponent<ShipController>().fireDelegate += FireDelayed;
        }
    }

    void Start()
    {
        ship = gameObject.GetComponentInParent<ShipController>();
    }
    #endregion

    public void FireDelayed()
    {
        //Debug.Log("Fire in weapon controller");

        if (weaponType == WeaponType.none)
            return;
        if (!gameObject.activeInHierarchy)
            return;
        if (ship.CheckEnergy() < weaponDefinition.shootEnergyCost)
            return;

/*        if (ship.gameObject.tag == "Player")
        {
            if (Time.time - lastShotTime < weaponDefinition.delayBetweenShots)
                return;
        }*/

        Invoke("Fire", shootDelayTime);
        ship.DecreaseEnergy(weaponDefinition.shootEnergyCost);
/*        lastShotTime = Time.time;*/
    }

    public void Fire()
    {
        ProjectileController projectile;
        float projectileSpeed = weaponDefinition.projectileVelocity;
        Vector3 projectileVelocity = Vector3.up * weaponDefinition.projectileVelocity;
        if (transform.up.y < 0) projectileVelocity.y = -projectileVelocity.y;        

        switch (weaponType)
        {
            case WeaponType.blaster:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.FromToRotation(Vector3.up, gameObject.transform.up), projectileSpeed);
                projectile.gameObject.SetActive(true);
                break;

            case WeaponType.spread:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.FromToRotation(Vector3.up, gameObject.transform.up), projectileSpeed);
                projectile.gameObject.SetActive(true);

                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.AngleAxis(10, gameObject.transform.forward), projectileSpeed);
                projectile.gameObject.SetActive(true);

                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.AngleAxis(-10, gameObject.transform.forward), projectileSpeed);
                projectile.gameObject.SetActive(true);
                break;

            case WeaponType.phaser:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.FromToRotation(Vector3.up, gameObject.transform.up), projectileSpeed);
                projectile.gameObject.SetActive(true);
                break;

            case WeaponType.enemyGun:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.FromToRotation(Vector3.up, gameObject.transform.up), projectileSpeed);
                projectile.gameObject.SetActive(true);
                break;
        }

/*        if (audioController != null)
            audioController.PlayClip(weaponDefinition.shootFX, 0.9f, 1.1f);*/
    }

    private ProjectileController GetProjectileFromPool()
    {
        GameObject go = ObjectPooler.Instance.GetPooledObject();

        ProjectileController projectile = go.GetComponent<ProjectileController>();
        projectile.SetWeaponType(weaponDefinition);

        if (transform.parent.gameObject.tag == "Player")
        {
            go.tag = "ProjectilePlayer";
            go.layer = LayerMask.NameToLayer("ProjectilePlayer");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        return (projectile);
    }

    /// <summary>
    /// Legacy, creates new gameobject for projectily
    /// </summary>
    /// <returns></returns>
    public ProjectileController MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(weaponDefinition.projectilePrefab);
        if (transform.parent.gameObject.tag == "Player")
        {
            go.tag = "ProjectilePlayer";
            go.layer = LayerMask.NameToLayer("ProjectilePlayer");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        go.transform.position = collar.transform.position;
        go.transform.SetParent(this.gameObject.transform, true);

        ProjectileController projectile = go.GetComponent<ProjectileController>();
        projectile.SetWeaponType(weaponDefinition);

        //Hero.S.energy -= Main.GetWeaponDefinition(p.type).projectileEnergyCost; // call For player stats

        return (projectile);
    }

}
