using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] Text textHealth;
    [SerializeField] Text textEnergy;
    [SerializeField] Text textShieldPower;
    [SerializeField] Text textShieldLevel;
    [SerializeField] Text textWeaponType;

    PlayerController player;
    ShipStats_SO playerStats;

    private void OnEnable()
    {
        player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        if (player == null)
            Debug.LogError("Player Controller not found by InGameUIManager");

        playerStats = player.gameObject.GetComponent<ShipStats>().shipDefinition;
        if (playerStats == null)
            Debug.LogError("Player Stats not found by InGameUIManager");

        SetupHealth(playerStats.currentHealth);
        SetupEnergy(playerStats.currentEnergy);
        SetupShieldPower(playerStats.currentShieldPower);
        SetupShieldLevel(playerStats.currentShieldLevel);
        SetUpWeaponType(playerStats.currentWeapon);

        playerStats.OnHealthChange.AddListener(OnHealthChangeHandler);
        playerStats.OnEnergyChange.AddListener(OnEnergyChangeHandler);
        playerStats.OnShieldPowerChange.AddListener(OnShieldPowerChangeHandler);
        playerStats.OnShieldLevelChange.AddListener(OnShieldLevelChangeHandler);
        playerStats.OnWeaponTypeChange.AddListener(OnWeaponTypeChangeHandler);
    }

    #region Deal with Sats changes.
    //Separate functions to have ability to add some FX
    private void SetupHealth(int value)
    {
        textHealth.text = value.ToString();
    }

    private void SetupEnergy(int value)
    {
        textEnergy.text = value.ToString();
    }

    private void SetupShieldPower(int value)
    {
        textShieldPower.text = value.ToString();
    }

    private void SetupShieldLevel(int value)
    {
        textShieldLevel.text = value.ToString();
    }

    private void SetUpWeaponType(WeaponType weapon)
    {
        textWeaponType.text = weapon.ToString();
    }
    #endregion

    #region Events from player ShipStat and Handlers

    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChange.RemoveListener(OnHealthChangeHandler);
            playerStats.OnEnergyChange.RemoveListener(OnEnergyChangeHandler);
            playerStats.OnShieldPowerChange.RemoveListener(OnShieldPowerChangeHandler);
            playerStats.OnShieldLevelChange.RemoveListener(OnShieldLevelChangeHandler);
            playerStats.OnWeaponTypeChange.RemoveListener(OnWeaponTypeChangeHandler);
        }
    }
    public void OnHealthChangeHandler(int health)
    {
        SetupHealth(playerStats.currentHealth);
    }

    public void OnEnergyChangeHandler(int energy)
    {
        SetupEnergy(playerStats.currentEnergy);
    }

    public void OnShieldPowerChangeHandler(int shieldPower)
    {
        SetupShieldPower(playerStats.currentShieldPower);
    }

    public void OnShieldLevelChangeHandler(int shieldLevel)
    {
        SetupShieldLevel(playerStats.currentShieldLevel);
    }

    public void OnWeaponTypeChangeHandler(WeaponType weapon)
    {
        SetUpWeaponType(playerStats.currentWeapon);
    }

    #endregion
}
