using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : Manager<SettingsController>
{
    [Header("Set in Inspector")]
    [SerializeField] private OptionsSet_SO defaultOptionsSet;

    [Header("Set Dynamicaly")]
    [SerializeField] private OptionsSet_SO activeOptionsSet;
    [SerializeField] private int tempFontSizeAddition;
    [SerializeField] private Color tempFontColor;

    private List<Text> texts;
    private Dictionary<Text, int> inittialTextSizes;
    private Dictionary<Text, Color> initialTextColors;



    #region Initial SetUp
    public override void Awake()
    {
        base.Awake();

        if (defaultOptionsSet != null)
            activeOptionsSet = Instantiate(defaultOptionsSet);

        texts = new List<Text>();
        inittialTextSizes = new Dictionary<Text, int>();
        initialTextColors = new Dictionary<Text, Color>();
    }
    void Start()
    {
        ClearAllDictionaries();

        SetUpTextDitionaries();
    }

    #endregion


    private void ClearAllDictionaries()
    {
        texts.Clear();
        inittialTextSizes.Clear();
        initialTextColors.Clear();
    }

    /// <summary>
    /// Search for text components in children objects and save backup of there font size and color settings;
    /// </summary>
    private void SetUpTextDitionaries()
    {
        Component[] foundTexts = GetComponentsInChildren(typeof(Text),true);
        if (foundTexts == null)
        {
            Debug.LogWarning("No Text components found");
            return;
        }
 
        foreach (Text text in foundTexts)
        {
            if (!text.resizeTextForBestFit)
            {
                texts.Add(text);
                Debug.Log("Text added to list");
            }
        }

        if (texts == null)
        {
            Debug.LogWarning("No Text components in text list");
            return;
        }

        foreach (Text text in texts)
        {
            inittialTextSizes.Add(text, text.fontSize);
            initialTextColors.Add(text, text.color);
        }
    }

    public void SetUpFontSizes(int value)
    {
        Debug.Log("SetUpFontSizes in SettingsController called");
        tempFontSizeAddition = value;
        foreach (Text text in texts)
        {
            text.fontSize = inittialTextSizes[text] + tempFontSizeAddition;
        }
    }

    public void SetUpFontColors(Color value)
    {
        tempFontColor = value;
        foreach (Text text in texts)
        {
            text.color = tempFontColor;
        }
    }

    public void ApplyTempSettings()
    {
        activeOptionsSet.fontSizeAddition = tempFontSizeAddition;
        activeOptionsSet.fontColor = tempFontColor;
    }

    public void CancelTempSettings()
    {
        SetUpFontSizes(activeOptionsSet.fontSizeAddition);
        SetUpFontColors(activeOptionsSet.fontColor);
    }

    public void SetDefaultSettings()
    {
        SetUpFontSizes(defaultOptionsSet.fontSizeAddition);
        SetUpFontColors(defaultOptionsSet.fontColor);
    }
}
