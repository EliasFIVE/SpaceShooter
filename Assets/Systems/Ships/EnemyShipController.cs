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
            StartCoroutine(EnemyFire(1f));
            //StartCoroutine(EnemyFire(WeaponManager.Instance.GetWeaponDefinition(weapon).delayBetweenShots));
        }
    }

    
/*    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                //≈сли вражеский корабль за границей экрана - не наносить ему повреждений
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }


                ShowDamage();
                Main.enemyHit++;
                audio.PlayOneShot(hitFX, 0.5F);

                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    if (!notifiedOfDestrucion) { Main.S.ShipDestroyed(this); }
                    notifiedOfDestrucion = true;
                    Die();
                }
                Destroy(otherGO);
                break;

            case "Neutral":
                Die();
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }*/
    IEnumerator EnemyFire(float delaytime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delaytime);
            Fire();
        }
    }
}
