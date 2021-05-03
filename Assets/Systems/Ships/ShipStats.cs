using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public ShipStats_SO shipDefinition_Template;
    public ShipStats_SO shipDefinition;

    private float speed;
    public float Speed
    {
        get { return speed; }
    }
    private void Awake()
    {
        if (shipDefinition_Template != null)
            shipDefinition = Instantiate(shipDefinition_Template);
    }
    private void Start()
    {
        speed = shipDefinition.speed;

        shipDefinition.currentHealth = shipDefinition.maxHealth;
        shipDefinition.currentEnergy = shipDefinition.maxEnergy;
    }

    #region StatControllers
    public void SetActiveWeapon(WeaponType type)
    {
        shipDefinition.SetActiveWeapon(type);
    }
    public void ApplyHealth(int amount)
    {
        shipDefinition.ApplyHealth(amount);
    }

    public void ApplyEnergy(int amount)
    {
        shipDefinition.ApplyEnergy(amount);
    }

    public void TakeDamage(int amount)
    {
        shipDefinition.TakeDamage(amount);
    }

    public void TakeEnergy(int amount)
    {
        shipDefinition.DecreaseEnergy(amount);
    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return shipDefinition.currentHealth;
    }

    public int GetEnergy()
    {
        return shipDefinition.currentEnergy;
    }

    public int GetEnergyGeneration()
    {
        return shipDefinition.energyGeneration;
    }

    public int GetShieldEnergy()
    {
        return shipDefinition.currentShieldEnergy;
    }

    public int GetShieldLevel()
    {
        return shipDefinition.shieldLevel;
    }

    public WeaponType GetActiveWeapon()
    {
        return shipDefinition.currentWeapon;
    }

    public WeaponType GetDefaultWeapon()
    {
        return shipDefinition.initialWeapon;
    }
    #endregion
}
