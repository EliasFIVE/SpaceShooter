using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationPerSecond = 0.1f;
    public bool rechargeActive = true;

    [Header("Set Dynamically")]
    [SerializeField] private int shieldLevel = 0;
    public int ShieldLevel
    {
        get { return shieldLevel; }
    }

    private ShipStats ship;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        ship = gameObject.GetComponentInParent<ShipStats>();

        ResetShieldLevel();

        StartCoroutine(RechargeCoroutine());
    }

    void Update()
    {
        float rZ = -(rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }

    private void ResetShieldLevel()
    {
        shieldLevel = ship.GetShieldLevel();
        material.mainTextureOffset = new Vector2(0.2f * shieldLevel, 0);

        //Because of coroutine delay (i think) in some cases appear non 0 shield energy whis 0 shield level
        //So here are control checks to avoid this bug
        if (shieldLevel == 0)
        {
            StopCoroutine(RechargeCoroutine());
            rechargeActive = false;
            ship.TakeShieldPower(ship.GetShieldPower());
            return;
        }

        if (shieldLevel != 0 && !rechargeActive)
        {
            rechargeActive = true;
            StartCoroutine(RechargeCoroutine());
            return;
        }
    }

    public void AbsorbDamage(int damage)
    {
        if (shieldLevel == 0)
        {
            Debug.LogWarning("Shield with 0 level is trying to absorb damage");
            return;
        }
        
        if((ship.GetShieldPower() - damage) < 0)
        {
            ship.DecreaseShieldLevel();
            ResetShieldLevel();

        } else if ((ship.GetShieldPower() - damage) == 0)
        {
            ship.TakeShieldPower(damage);
            ResetShieldLevel();
        }
        else
        {
            ship.TakeShieldPower(damage);
        }
    }

    public void PowerUpShield()
    {
        ship.IncreseShieldLevel();
        ResetShieldLevel();
    }

    IEnumerator RechargeCoroutine()
    {
        while (rechargeActive)
        {
            yield return new WaitForSeconds(1f);
            ship.RechargeShield();
        }
    }

}
