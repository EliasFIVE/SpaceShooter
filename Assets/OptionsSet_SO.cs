using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewOptionsSet", menuName = "Options/OptionsSet", order = 1)]
public class OptionsSet_SO : ScriptableObject
{
    public int fontSizeAddition;
    public Color fontColor;
}
