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
    void Start()
    {
        applyButton.onClick.AddListener(HandleApplyClicked);
        cancelButton.onClick.AddListener(HandleCancelClicked);
        defaultButton.onClick.AddListener(HandleDefaultClicked);
    }

    public void SetControllersByOptionsSet(OptionsSet_SO settings)
    {
        fontSizeSlider.value = settings.fontSizeAddition;
        fontColorDropdown.value = fontColors.IndexOf(settings.fontColor);        
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

    public void SetFontSize (float value)
    {
        //Debug.Log("OptionsMenu SetFontSize value: " + value.ToString());
        SettingsController.Instance.SetUpFontSizes((int)value);
    }

    public void SetFontColor (Dropdown dropdown)
    {
        Color color = fontColors[dropdown.value];
        SettingsController.Instance.SetUpFontColors(color);
    }

    public void SetColorTheme (Dropdown value)
    {
        Debug.LogWarning("Not Implemented");
    }

    public void SetMasterVolume(float value)
    {
        Debug.LogWarning("Not Implemented");
    }

    public void SetMusicVolume(float value)
    {
        Debug.LogWarning("Not Implemented");
    }

    public void SetSoundFXVolume(float value)
    {
        Debug.LogWarning("Not Implemented");
    }
}
