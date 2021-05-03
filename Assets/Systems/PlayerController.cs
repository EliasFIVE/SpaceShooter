using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    // Movement settings
    [Header("Set in Inspector")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float rollMult = -40;
    [SerializeField] private float pitchMult = 20;

    [SerializeField] private WeaponController[] weapons; 
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    //Move to stats SO
    /*    public float energyMax = 100f;
        public float energyGeneration = 1f;
        public bool weaponOverheat = false;
        public float coolDownTime = 10f;

        private float _shieldLevel = 1;*/

    void Start()
    {
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);

        //Debug
        //SetWeaponTypeForAll(WeaponType.blaster);
    }

    void Update()
    {
        float inputHoriz = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputHoriz != 0 || inputVertical != 0)
        {
            MoveShip(inputHoriz, inputVertical);
        }

        if (Input.GetAxis("Jump") == 1)
        {
            Fire();
        }
    }

    /// <summary>
    /// Move ship by user input
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void MoveShip(float x, float y)
    {
        Vector3 pos = transform.position;
        pos.x += x * speed * Time.deltaTime;
        pos.y += y * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate ship when move
        transform.rotation = Quaternion.Euler(y * pitchMult, x * rollMult, 0);
    }

    #region Weapon interaction
    private void Fire()
    {
        if(fireDelegate != null)
        {
            fireDelegate();
        }
    }

    /// <summary>
    /// Clear weapon array
    /// </summary>
    void ClearWeapons()
    {
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
    void SetWeaponTypeForAll(WeaponType type)
    {
        foreach (WeaponController weapon in weapons)
        {
            weapon.SetType(type);
        }
    }

    #endregion

    // Not destroy object if death, but make it inactive
    public void OnDestruction(GameObject destroyer)
    {
        gameObject.SetActive(false);
    }
}
