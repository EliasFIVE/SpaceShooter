using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShipController : ShipController
{
    //MoveShip mover;
    BoundsCheck bound;
    UnityEvent ShipDeathEvent;

    public override void Start()
    {
        base.Start();
        bound = gameObject.GetComponent<BoundsCheck>();

        if (EnemiesManager.Instance != null)
            ShipDeathEvent.AddListener(EnemiesManager.Instance.OnEmemyDeath);

        if (weapons.Length != 0)
        {
            WeaponType weapon = stats.GetDefaultWeapon();
            SetWeaponTypeForAll(weapon);
            StartCoroutine(EnemyFire(WeaponManager.Instance.GetWeaponDefinition(weapon).delayBetweenShots));
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

    private void OnDestroy()
    {
        ShipDeathEvent.Invoke();
    }


    IEnumerator EnemyFire(float delaytime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delaytime);
            if (bound.IsOnScreen)
                Fire();
        }
    }
}
