using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    public Weapon_SO[] weaponDefinitions; //list of all weapon types
    static Dictionary<WeaponType, Weapon_SO> WEAP_DICT;

    private void WeaponDictSetUp()
    {
        WEAP_DICT = new Dictionary<WeaponType, Weapon_SO>();
        foreach (Weapon_SO def in weaponDefinitions)
        {
            WEAP_DICT[def.weaponType] = def;
        }
    }
    public Weapon_SO GetWeaponDefinition(WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        else
        {
            Debug.LogError("Weapon with this type not defined in manager");
            return null;
        }

    }

    override public void Awake()
    {
        base.Awake();
        WeaponDictSetUp();
    }
}
