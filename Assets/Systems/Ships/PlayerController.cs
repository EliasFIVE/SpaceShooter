using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipController
{
    // Movement settings
    [Header("Set in Inspector")]

    [SerializeField] private float rollMult = -40;
    [SerializeField] private float pitchMult = 20;

    private float lastShotTime; //Time from last shot

    public override void Start()
    {
        base.Start();

        ActivateFirstInactiveWeapon(stats.GetDefaultWeapon());
        //ActivateWeaponAndSetType(0, stats.GetDefaultWeapon());
        //weapons[0].SetType(stats.GetDefaultWeapon());
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
            PlayerFire();
        }
    }

    private void PlayerFire()
    {
        if (Time.time - lastShotTime < WeaponManager.Instance.GetWeaponDefinition(stats.GetActiveWeapon()).delayBetweenShots)
            return;
        Fire();
        lastShotTime = Time.time;
    }
    /// <summary>
    /// Move ship by user input
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void MoveShip(float x, float y)
    {
        Vector3 pos = transform.position;
        pos.x += x * stats.Speed * Time.deltaTime;
        pos.y += y * stats.Speed * Time.deltaTime;
        transform.position = pos;

        //Rotate ship when move
        transform.rotation = Quaternion.Euler(y * pitchMult, x * rollMult, 0);
    }
    // Not destroy object if death, but make it inactive
    public void OnDestruction(GameObject destroyer)
    {
        gameObject.SetActive(false);
    }
}
