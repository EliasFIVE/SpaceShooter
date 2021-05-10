using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour, IDamagable
{
    [HideInInspector] public ShipStats stats;

    [SerializeField] protected WeaponController[] weapons;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    protected bool energyGenerationActive = true;

    protected Vector3 initialPosition;
    public virtual void Start()
    {
        stats = gameObject.GetComponent<ShipStats>();

        initialPosition = transform.position;

        if (weapons.Length != 0)
        {
            ClearWeapons();
        }

        if (energyGenerationActive)
            StartCoroutine(GenerateEnergy());
    }

    #region Weapon interaction
    protected void Fire()
    {
        fireDelegate?.Invoke();
    }

    /// <summary>
    /// Deactivate all weapon
    /// </summary>
    public void ClearWeapons()
    {
        stats.SetActiveWeapon(WeaponType.none);

        foreach (WeaponController weapon in weapons)
        {
            weapon.SetType(WeaponType.none);
            //weaponOverheatTime = 0f;
            //energy = coolDownTime;
        }
    }

    /// <summary>
    /// Set weapon type for all weapons
    /// </summary>
    /// <param name="type"></param>
    protected void SetWeaponTypeForAll(WeaponType type)
    {
        stats.SetActiveWeapon(type);

        foreach (WeaponController weapon in weapons)
        {
            weapon.SetType(type);
        }
    }

    /// <summary>
    /// Activate weapon by given number in list and sets weapontype by type
    /// </summary>
    /// <param name="weaponNumber"></param>
    /// <param name="type"></param>
    protected void ActivateWeaponAndSetType(int weaponNumber, WeaponType type)
    {
        stats.SetActiveWeapon(type);
        weapons[weaponNumber].SetType(type);
    }

    /// <summary>
    /// Set weapon type for active weapon objects
    /// </summary>
    /// <param name="type"></param>
    protected void SetWeaponTypeForActive(WeaponType type)
    {
        stats.SetActiveWeapon(type);
        foreach (WeaponController weapon in weapons)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                weapon.SetType(type);
            }
        }
    }

    /// <summary>
    /// Search for inactive in weapon list, activate first found and sets weapontype by type
    /// </summary>
    /// <param name="type"></param>
    public void ActivateFirstInactiveWeapon(WeaponType type)
    {
        stats.SetActiveWeapon(type);
        WeaponController weapon = GetEmptyWeaponSlot();
        if (weapon != null)
        {
            weapon.SetType(type);
        }
    }

    WeaponController GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].WeaponType == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }
    #endregion

    #region Energy
    public void DecreaseEnergy(int amount)
    {
        stats.DecreaseEnergy(amount);
    }

    public int CheckEnergy()
    {
        return stats.GetEnergy();
    }

    IEnumerator GenerateEnergy()
    {
        while (energyGenerationActive)
        {
            yield return new WaitForSeconds(1f);
            stats.IncreaseEnergy(stats.GetEnergyGeneration());
        }
    }
    #endregion

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);

        Debug.LogFormat("{0} take {1} damage", gameObject.name, damage.ToString());

        if (stats.GetHealth() <= 0)
        {
            Debug.LogFormat("{0} health less then 0", gameObject.name);

            StopAllCoroutines();

            var destructibles = GetComponents(typeof(IDestructable));
            foreach (IDestructable d in destructibles)
                d.OnDestruction(transform.position);
        }
    }

    protected void MoveToInitialPosition()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }

    protected void MoveToInitialPosition(float timeDelay)
    {
        Invoke("MoveToInitialPosition", timeDelay);
        gameObject.SetActive(false);
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
                shield.AbsorbDamage(1000); //damage amount big enuogth to drop shield level
                TakeDamage(1000);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == gameObject.tag)
            return;
        if (collision.gameObject.GetComponent<ProjectileController>() != null)
            return;

        Debug.Log("Ship collision");
        TakeDamage(1000); //damage amount big enuogth to kill 
        //OnCollisionEnter event will occur for both ships, so no need to damage collision.gameobject here
    }
}
