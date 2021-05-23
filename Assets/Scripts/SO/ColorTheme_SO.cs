using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorTheme", menuName = "Options/ColorTheme", order = 2)]
public class ColorTheme_SO : ScriptableObject
{
    [SerializeField] public Color imageColor;
    [SerializeField] public Color buttonColor;
    [SerializeField] public Color sliderColor;
    [SerializeField] public Color dropdownColor;
    [SerializeField] public Color fontColor;
}
