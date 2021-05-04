using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewStats", menuName = "ShipStats", order = 1)]
public class ShipStats_SO : ScriptableObject
{
    public Events.EventIntegerEvent OnPlayerDamaged;
    public Events.EventIntegerEvent OnPlayerGainedHealth;
    public UnityEvent OnPlayerDeath;

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

    public int maxShieldLevel = 0;
    public int shieldLevel = 0;

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
            OnPlayerGainedHealth.Invoke(amount); 
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
    }

    public void IncreaseShieldLevel()
    {
        if (shieldLevel++ > maxShieldLevel)
        {
            shieldLevel = maxShieldLevel;
            currentShieldPower = maxShieldPower;
        }
        else
        {
            shieldLevel++;
        }
    }

    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (isPlayer)
            OnPlayerDamaged.Invoke(amount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
    }

    public void DecreaseShieldPower(int amount)
    {
        if ((currentShieldPower - amount) < 0)
        {
            if(shieldLevel != 0)
            {
                DecreaseShieldLevel();
                currentShieldPower = maxShieldPower;
            }
        }
        else
        {
            currentShieldPower -= amount;
        }
    }

    public void DecreaseShieldLevel()
    {
        if (shieldLevel == 0)
        {
            Debug.LogWarning("Trying to decrease 0 shield level");
            shieldLevel = 0;
        } else
        {
            shieldLevel--;
            currentShieldPower = maxShieldPower;
        }
    }
    #endregion

    public void SetActiveWeapon(WeaponType type)
    {
        currentWeapon = type;
    }
    private void Death()
    {
        if (isPlayer)
            OnPlayerDeath.Invoke();
    }

}


