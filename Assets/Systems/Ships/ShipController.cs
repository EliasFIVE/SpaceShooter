using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour, IDamagable
{
    protected ShipStats stats;

    [SerializeField] protected WeaponController[] weapons;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    protected bool energyGenerationActive = true;

    public virtual void Start()
    {
        stats = gameObject.GetComponent<ShipStats>();

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
    protected void ClearWeapons()
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
    #endregion

    #region Energy
    public void DecreaseEnergy(int amount)
    {
        stats.TakeEnergy(amount);
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

        Debug.LogFormat("{0} take {1} damage value", gameObject.name, damage.ToString());

        if (stats.GetHealth() <= 0)
        {
            Debug.LogFormat("{0} health less then 0", gameObject.name);

            StopAllCoroutines();

            var destructibles = GetComponents(typeof(IDestructable));
            foreach (IDestructable d in destructibles)
                d.OnDestruction(transform.position);
        }
    }
}
