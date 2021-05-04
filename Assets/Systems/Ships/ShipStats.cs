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
        shipDefinition.currentShieldPower = shipDefinition.maxShieldPower;
    }

    #region StatControllers
    public void SetActiveWeapon(WeaponType type)
    {
        shipDefinition.SetActiveWeapon(type);
    }
    public void IncreaseHealth(int amount)
    {
        shipDefinition.IncreaseHealth(amount);
    }

    public void IncreaseEnergy(int amount)
    {
        shipDefinition.IncreaseEnergy(amount);
    }

    public void IncreaseShieldPower(int amount)
    {
        shipDefinition.IncreaseShieldPower(amount);
    }

    public void IncreseShieldLevel()
    {
        shipDefinition.IncreaseShieldLevel();
    }

    public void TakeDamage(int amount)
    {
        shipDefinition.TakeDamage(amount);
    }

    public void TakeEnergy(int amount)
    {
        shipDefinition.DecreaseEnergy(amount);
    }

    public void TakeShieldPower(int amount)
    {
        shipDefinition.DecreaseShieldPower(amount);
    }

    public void DecreaseShieldLevel()
    {
        shipDefinition.DecreaseShieldLevel();
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

    public int GetShieldPower()
    {
        return shipDefinition.currentShieldPower;
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
