using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : Manager<SettingsController> //Rename to Manager, move to sepparate object
{
    [Header("Set in Inspector")]
    [SerializeField] private OptionsSet_SO defaultOptionsSet;
 
    [Header("Set Dynamicaly")]
    [SerializeField] private OptionsSet_SO activeOptionsSet;
    [SerializeField] private int tempFontSizeAddition;
    [SerializeField] private Color tempFontColor;
    [SerializeField] private ColorTheme_SO tempColorTheme;
    [SerializeField] private float tempMasterVolume;
    [SerializeField] private float tempMusicVolume;
    [SerializeField] private float tempSoundFXVolume;

    private List<Text> texts;
    private Dictionary<Text, int> inittialTextSizes;
    private Dictionary<Text, Color> initialTextColors;

    public OptionsSet_SO DefaultOptionsSet
    {
        get { return defaultOptionsSet; }
    }
    public OptionsSet_SO ActiveOptionsSet
    {
        get { return activeOptionsSet; }
    }

    public Events.EventColorThemeChange ColorThemeChange;

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

        SetUpTextDictionaries();

        SetDefaultSettings();
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
    private void SetUpTextDictionaries()
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
                //Debug.Log("Text added to list");
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
        //Debug.Log("SetUpFontSizes in SettingsController called");

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

            if (text.gameObject.GetComponent<TextBlink>() != null)
            {
                text.gameObject.GetComponent<TextBlink>().color1 = tempFontColor;
            }
        }
    }

    public void SetUpColorTheme(ColorTheme_SO colorTheme)
    {
        tempColorTheme = colorTheme;

        SetUpFontColors(colorTheme.fontColor);
        ColorThemeChange.Invoke(colorTheme);
    }

    public void SetUpMasterVolume(float volume)
    {
        tempMasterVolume = volume;
        SoundManager.Instance.SetMasterVolume(volume);
    }

    public void SetUpMusicVolume(float volume)
    {
        tempMusicVolume = volume;
        SoundManager.Instance.SetMusicVolume(volume);
    }

    public void SetUpFXVolume(float volume)
    {
        tempSoundFXVolume = volume;
        SoundManager.Instance.SetFXVolume(volume);
    }

    public void ApplyTempSettings()
    {
        activeOptionsSet.fontSizeAddition = tempFontSizeAddition;
        activeOptionsSet.fontColor = tempFontColor;
        activeOptionsSet.colorTheme = tempColorTheme;
        activeOptionsSet.masterVolume = tempMasterVolume;
        activeOptionsSet.musicVolume = tempMusicVolume;
        activeOptionsSet.soundFXVolume = tempSoundFXVolume;
    }

    public void CancelTempSettings()
    {
        SetUpFontSizes(activeOptionsSet.fontSizeAddition);
        SetUpFontColors(activeOptionsSet.fontColor);
        SetUpColorTheme(activeOptionsSet.colorTheme);
        SetUpMasterVolume(activeOptionsSet.masterVolume);
        SetUpMusicVolume(activeOptionsSet.musicVolume);
        SetUpFXVolume(activeOptionsSet.soundFXVolume);
    }

    public void SetDefaultSettings()
    {
        SetUpFontSizes(defaultOptionsSet.fontSizeAddition);
        //SetUpFontColors(defaultOptionsSet.colorTheme.fontColor); //Color theme has higher priority
        SetUpColorTheme(defaultOptionsSet.colorTheme);
        SetUpMasterVolume(defaultOptionsSet.masterVolume);
        SetUpMusicVolume(defaultOptionsSet.musicVolume);
        SetUpFXVolume(defaultOptionsSet.soundFXVolume);
    }
}
