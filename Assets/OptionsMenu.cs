using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button applyButton;
    [SerializeField] private Button cancelButton;

    void Start()
    {
        applyButton.onClick.AddListener(HandleApplyClicked);
        cancelButton.onClick.AddListener(HandleCancelClicked);
    }

    private void HandleCancelClicked()
    {
        // Add Resume to previous settings
        UIManager.Instance.ToggleOptionsMenu();
    }

    private void HandleApplyClicked()
    {
        // Apply new settings to current settiongs SO
        UIManager.Instance.ToggleOptionsMenu();
    }
}
