using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button cancelButton;

    void Start()
    {
        applyButton.onClick.AddListener(HandleApplyClicked);
        cancelButton.onClick.AddListener(HandleCancelClicked);
    }

    private void HandleCancelClicked()
    {
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
        Debug.Log("OptionsMenu SetFontSize value: " + value.ToString());
        SettingsController.Instance.SetUpFontSizes((int)value);
    }

    public void SetFontColor (Dropdown value)
    {

    }

    public void SetColorTheme (Dropdown value)
    {

    }

    public void SetMasterVolume(float value)
    {

    }

    public void SetMusicVolume(float value)
    {

    }

    public void SetSoundFXVolume(float value)
    {

    }
}
