using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewStats", menuName = "ShipStats", order = 1)]
public class ShipStats_SO : ScriptableObject
{

    [HideInInspector] public Events.EventIntegerEvent OnHealthChange;
    [HideInInspector] public Events.EventIntegerEvent OnEnergyChange;
    [HideInInspector] public Events.EventIntegerEvent OnShieldPowerChange;
    [HideInInspector] public Events.EventIntegerEvent OnShieldLevelChange;
    [HideInInspector] public Events.EventWeaponTypeEvent OnWeaponTypeChange;

    [HideInInspector] public UnityEvent OnPlayerDeath;

    public bool isPlayer = false;

    public WeaponType initialWeapon;
    public WeaponType currentWeapon;

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxEnergy = 0;
    public int currentEnergy = 0;
    public int energyGeneration = 0;

    public int maxShieldPower = 0;
    public int currentShieldPower = 0;
    public int shieldRechargeSpeed = 0;

    public int maxShieldLevel = 0;
    public int currentShieldLevel = 0;

    public int speed;

    #region Stat Increasers
    public void IncreaseHealth(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        if (isPlayer)
            OnHealthChange.Invoke(currentHealth); 
    }

    public void IncreaseEnergy(int amount)
    {
        if ((currentEnergy + amount) > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy += amount;
        }

        if (isPlayer)
            OnEnergyChange.Invoke(currentEnergy);
    }

    public void IncreaseShieldPower(int amount)
    {
        if ((currentShieldPower + amount) > maxShieldPower)
        {
            currentShieldPower = maxShieldPower;
        }
        else
        {
            currentShieldPower += amount;
        }

        if (isPlayer)
            OnShieldPowerChange.Invoke(currentShieldPower);
    }

    public void IncreaseShieldLevel()
    {
        if (currentShieldLevel++ > maxShieldLevel)
        {
            currentShieldLevel = maxShieldLevel;
            currentShieldPower = maxShieldPower;
        }
        else
        {
            currentShieldLevel++;
        }

        if (isPlayer)
            OnShieldLevelChange.Invoke(currentShieldLevel);
    }

    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }

        if (isPlayer)
            OnHealthChange.Invoke(currentHealth);
    }

    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }

        if (isPlayer)
            OnEnergyChange.Invoke(currentEnergy);
    }

    public void DecreaseShieldPower(int amount)
    {
        if ((currentShieldPower - amount) <= 0)
        {
            currentShieldPower = 0;
            DecreaseShieldLevel();
        }
        else
        {
            currentShieldPower -= amount;
        }

        if (isPlayer)
            OnShieldPowerChange.Invoke(currentShieldPower);
    }

    public void DecreaseShieldLevel()
    {
        if (currentShieldLevel == 0)
        {
            currentShieldLevel = 0;
        } else
        {
            currentShieldLevel--;
            if(currentShieldLevel != 0)
                currentShieldPower = maxShieldPower;
        }

        if (isPlayer)
            OnShieldLevelChange.Invoke(currentShieldPower);
    }
    #endregion

    public void SetActiveWeapon(WeaponType type)
    {
        currentWeapon = type;

        if (isPlayer)
            OnWeaponTypeChange.Invoke(currentWeapon);
    }
    private void Death()
    {
        if (isPlayer)
            OnPlayerDeath.Invoke();
    }

}


