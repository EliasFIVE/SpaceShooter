using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button defaultButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private Slider fontSizeSlider;
    [SerializeField] private Dropdown fontColorDropdown;
    [SerializeField] private Dropdown colorThemeDropdown;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;

    [Header("Order must match FontColor dropdown menu")]
    [SerializeField] private List<Color> fontColors;
    [SerializeField] private List<ColorTheme_SO> colorThemes;
    void Start()
    {
        applyButton.onClick.AddListener(HandleApplyClicked);
        cancelButton.onClick.AddListener(HandleCancelClicked);
        defaultButton.onClick.AddListener(HandleDefaultClicked);

        SetControllersByOptionsSet(SettingsController.Instance.DefaultOptionsSet);
    }

    public void SetControllersByOptionsSet(OptionsSet_SO settings)
    {
        fontSizeSlider.value = settings.fontSizeAddition;
        colorThemeDropdown.value = colorThemes.IndexOf(settings.colorTheme);
        fontColorDropdown.value = fontColors.IndexOf(settings.fontColor);
        masterVolumeSlider.value = settings.masterVolume;
        musicVolumeSlider.value = settings.musicVolume;
        soundFXVolumeSlider.value = settings.soundFXVolume;
    }

    private void HandleDefaultClicked()
    {
        SetControllersByOptionsSet(SettingsController.Instance.DefaultOptionsSet);
        SettingsController.Instance.SetDefaultSettings();
    }

    private void HandleCancelClicked()
    {
        SetControllersByOptionsSet(SettingsController.Instance.ActiveOptionsSet);
        SettingsController.Instance.CancelTempSettings();
        UIManager.Instance.ToggleOptionsMenu();
    }

    private void HandleApplyClicked()
    {
        SettingsController.Instance.ApplyTempSettings();
        UIManager.Instance.ToggleOptionsMenu();
    }

    public void HandleFontSizeSlider (float value)
    {
        //Debug.Log("OptionsMenu SetFontSize value: " + value.ToString());
        SettingsController.Instance.SetUpFontSizes((int)value);
    }

    public void HandleFontColorDropdown (Dropdown dropdown)
    {
        Color color = fontColors[dropdown.value];
        SettingsController.Instance.SetUpFontColors(color);
    }

    public void HandleColorThemeDropdown (Dropdown dropdown)
    {
        ColorTheme_SO theme = colorThemes[dropdown.value];
        fontColorDropdown.value = fontColors.IndexOf(theme.fontColor);
        SettingsController.Instance.SetUpColorTheme(theme);
    }

    public void HandleMasterVolumeSlider(float value)
    {
        if(value == -40)
        {
            SettingsController.Instance.SetUpMasterVolume(-80);
            return;
        }
        SettingsController.Instance.SetUpMasterVolume(value);
    }

    public void HandleMusicVolumeSlider(float value)
    {
        if (value == -40)
        {
            SettingsController.Instance.SetUpMusicVolume(-80);
            return;
        }
        SettingsController.Instance.SetUpMusicVolume(value);
    }

    public void HandleSoundFXVolumeSlider(float value)
    {
        if (value == -40)
        {
            SettingsController.Instance.SetUpFXVolume(-80);
            return;
        }
        SettingsController.Instance.SetUpFXVolume(value);
    }
}
