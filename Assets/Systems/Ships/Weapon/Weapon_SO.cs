using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    none,
    blaster, //default
    spread, //Shotgun
    phaser,  //machinegun
    //missile,
    //laser,
    //mine,
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon Item", order = 1)]
public class Weapon_SO :Item_SO
{
    [Header ("Weapon setup")]
    public WeaponType weaponType = WeaponType.none;
    public GameObject projectilePrefab;     //Projectile prefab
    public Color projectileColor = Color.white; //Projectile color

    public float delayBetweenShots = 0.1f;
    public float projectileVelocity = 20;
    public int damageOnHit = 1;

    public float projectileLifeTime = 10f;
    public int shootEnergyCost = 1;

    public AudioClip shootFX;
}
