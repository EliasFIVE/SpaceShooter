using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject collar; //Dummy object to show visual identification of current set weapon and to set initial projectile position
    private Renderer collarRend;

    [Header("Set In Inspector")]
    public float shootDelayTime = 0; //Set in inspector different time for guns in single ship to make them shoot one by one

    [Header("Set Dynamically")]
    [SerializeField] private WeaponType weaponType = WeaponType.none;
    [SerializeField] private Weapon_SO weaponDefinition;
    private float lastShotTime; //Time from last shot
    public WeaponType WeaponType
    {
        get { return (weaponType); }
        set { SetType(value); }
    }
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
        lastShotTime = 0;
    }

    private LocalAudioController audioController;

    #region Initial setup
    private void Awake()
    {
        collar = transform.Find("Collar").gameObject; //Not best descision: to find object by string name. Need to think of alt way. But don't want to set it manualy in inspector
        collarRend = collar.GetComponent<Renderer>();
    }
    void Start()
    {
        //WeaponType.none
        SetType(weaponType);

        //Find fireDelegate in root game object
        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<PlayerController>() != null)
        {
            rootGO.GetComponent<PlayerController>().fireDelegate += FireDelayed;
        }

        //Set AudioController
        if(gameObject.GetComponent<LocalAudioController>() != null)
        {
            audioController = gameObject.GetComponent<LocalAudioController>();
        } else
        {
            Debug.LogWarningFormat("AudioSource for weapon of object {0} not set.", gameObject.name);
        }
    }
    #endregion

    public void FireDelayed()
    {
        if (weaponType == WeaponType.none)
            return;
        if (!gameObject.activeInHierarchy)
            return;
        if (Time.time - lastShotTime < weaponDefinition.delayBetweenShots)
            return;

        Invoke("Fire", shootDelayTime);

        lastShotTime = Time.time;
    }

    ///Transfer to weapon definition SO???
    public void Fire()
    {
        ProjectileController projectile;
        Vector3 projectileVelocity = Vector3.up * weaponDefinition.projectileVelocity;
        if (transform.up.y < 0) projectileVelocity.y = -projectileVelocity.y;
        
        switch (weaponType)
        {
            case WeaponType.blaster:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.identity, projectileVelocity);
                projectile.gameObject.SetActive(true);
                break;

            case WeaponType.spread:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.identity, projectileVelocity);
                projectile.gameObject.SetActive(true);

                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.AngleAxis(10, Vector3.back), projectileVelocity);
                projectile.gameObject.SetActive(true);

                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.AngleAxis(-10, Vector3.back), projectileVelocity);
                projectile.gameObject.SetActive(true);
                break;

            case WeaponType.phaser:
                projectile = GetProjectileFromPool();
                projectile.ResetProjectile(collar.transform.position, Quaternion.identity, projectileVelocity);
                projectile.gameObject.SetActive(true);
                break;
        }

        if (audioController != null)
            audioController.PlayClip(weaponDefinition.shootFX, 0.9f, 1.1f);
    }

    private ProjectileController GetProjectileFromPool()
    {
        GameObject go = ObjectPooler.Instance.GetPooledObject();

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

        ProjectileController projectile = go.GetComponent<ProjectileController>();
        projectile.SetWeaponType(weaponDefinition);

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
