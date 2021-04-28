using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    none,
    shield,
    weapon
}
[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Bonus item", order = 1)]
public class Item_SO : ScriptableObject
{ 
    public ItemType type = ItemType.none;
    public string letter;                   //Letter for bonus item object visual identification
    public Color color = Color.white;       //Item Color
}
