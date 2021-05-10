using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShipController : ShipController
{
    //MoveShip mover;
    BoundsCheck bound;

    private WeaponType weapon;
    private bool weaponReady = false;


    private void OnEnable()
    {
        if (weaponReady)
        {
            Debug.Log("reenableFire");
            ActivateFire();
        }
    }
    public override void Start()
    {
        base.Start();

        if (gameObject.GetComponent<BoundsCheck>() != null)
        {
            bound = gameObject.GetComponent<BoundsCheck>();
        }
        else
        {
            Debug.LogWarningFormat("BoundsCheck not set to the enemy ship prefab of {0}", gameObject.name);
        }


        EnemiesManager enemiesManager = EnemiesManager.Instance;
        if (enemiesManager != null)
            stats.shipDefinition.OnShipDeath.AddListener(enemiesManager.OnEmemyDeath);

        if (weapons.Length != 0)
        {
            weapon = stats.GetDefaultWeapon();
            SetWeaponTypeForAll(weapon);
            weaponReady = true; //bugFix: need to reactivate fire after reenable of object, but onEnable runs before Start
            ActivateFire();
        }
    }

    private void Update()
    {
        if (bound.offDown)
        {
            MoveToInitialPosition(1f);
            bound.offDown = false;
        }
    }

    public void ActivateFire()
    {
        Debug.Log("ActivateFire");
        StartCoroutine(EnemyFire(WeaponManager.Instance.GetWeaponDefinition(weapon).delayBetweenShots));
    }

    IEnumerator EnemyFire(float delaytime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delaytime);
            if (bound.IsOnScreen)
            {
                //Debug.Log("EnemyFire in enemyshipcontroller");
                Fire();
            } 
        }
    }
}
