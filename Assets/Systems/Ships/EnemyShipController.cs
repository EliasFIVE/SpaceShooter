using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : ShipController
{
    public override void Start()
    {
        base.Start();

        if (weapons.Length != 0)
        {
            WeaponType weapon = stats.GetDefaultWeapon();
            SetWeaponTypeForAll(weapon);
            StartCoroutine(EnemyFire(WeaponManager.Instance.GetWeaponDefinition(weapon).delayBetweenShots));
        }
    }

    IEnumerator EnemyFire(float delaytime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delaytime);
            Fire();
        }
    }
}
