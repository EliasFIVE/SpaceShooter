using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : ShipController
{
    MoveShip mover;
    BoundsCheck bound;
    public override void Start()
    {
        base.Start();

        if (weapons.Length != 0)
        {
            bound = gameObject.GetComponent<BoundsCheck>();
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
            if(bound.IsOnScreen)
                Fire();
        }
    }
}
