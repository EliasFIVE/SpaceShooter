using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorThemeSetter: MonoBehaviour
{
    //dynamic component; 

    //using dynamic in this scrip results in an error in RuntimeBinder
    //searching for solution showed that the error is related to the use of the framework version 4.
    //framework version 4.5 must resolve this issue
    //for more versatility, I decided to abandon dynamic
    private enum ComponentType
    {
        BUTTON,
        SLIDER,
        DROPDOWN,
        IMAGE
    }
    ComponentType componentType;
    Button buttonComponent;
    Slider sliderComponent;
    Dropdown dropdownComponent;
    Image imageComponent;

    void Awake()
    {
        FindThemeComponent();
    }

    private void OnEnable()
    {
        SetColorByTheme(SettingsController.Instance.ActiveOptionsSet.colorTheme);
        SettingsController.Instance.ColorThemeChange.AddListener(SetColorByTheme);
    }
    private void Start()
    {
        //SettingsController.Instance.ColorThemeChange.AddListener(SetColorByTheme);
    }

    /*    private void FindThemeComponent()
        {
            if (gameObject.GetComponent<Button>() != null)
            {
                component = gameObject.GetComponent<Button>();
                return;
            }

            if (gameObject.GetComponent<Slider>() != null)
            {
                component = gameObject.GetComponent<Slider>();
                return;
            }

            if (gameObject.GetComponent<Dropdown>() != null)
            {
                component = gameObject.GetComponent<Dropdown>();
                return;
            }

            if (gameObject.GetComponent<Image>() != null)
            {
                component = gameObject.GetComponent<Image>();
                return;
            }*/
    /*    public void SetColorByTheme(ColorTheme_SO colorTheme)
        {
            Type type = component.GetType();

            if (type.Equals(typeof(Button)))
            {
                Button button = component;
                ColorBlock cb = button.colors;
                cb.normalColor = colorTheme.buttonColor;
                button.colors = cb;
                return;
            }

            if (type.Equals(typeof(Slider)))
            {
                Slider clider = component;
                ColorBlock cb = clider.colors;
                cb.normalColor = colorTheme.buttonColor;
                clider.colors = cb;
                return;
            }

            if (type.Equals(typeof(Dropdown)))
            {
                Dropdown dropdown = component;
                ColorBlock cb = dropdown.colors;
                cb.normalColor = colorTheme.buttonColor;
                dropdown.colors = cb;
                return;
            }

            if (type.Equals(typeof(Image)))
            {
                Image image = component;
                image.color = colorTheme.imageColor;
                return;
            }
        }*/

    private void FindThemeComponent()
    {
        if (gameObject.GetComponent<Button>() != null)
        {
            buttonComponent = gameObject.GetComponent<Button>();
            componentType = ComponentType.BUTTON;
            return;
        }

        if (gameObject.GetComponent<Slider>() != null)
        {
            sliderComponent = gameObject.GetComponent<Slider>();
            componentType = ComponentType.SLIDER;
            return;
        }

        if (gameObject.GetComponent<Dropdown>() != null)
        {
            dropdownComponent = gameObject.GetComponent<Dropdown>();
            componentType = ComponentType.DROPDOWN;
            return;
        }

        if (gameObject.GetComponent<Image>() != null)
        {
            imageComponent = gameObject.GetComponent<Image>();
            componentType = ComponentType.IMAGE;
            return;
        }
    }

    public void SetColorByTheme(ColorTheme_SO colorTheme)
    {
        switch (componentType)
        {       
            case (ComponentType.BUTTON):
                if (buttonComponent == null)
                {
                    Debug.LogWarningFormat("Component type {0} not set in UI object {1} ", 
                        componentType.ToString(), gameObject.name.ToString());
                    return;
                }
                ColorBlock bc = buttonComponent.colors;
                bc.normalColor = colorTheme.buttonColor;
                buttonComponent.colors = bc;
                break;

            case (ComponentType.SLIDER):
                if (sliderComponent == null)
                {
                    Debug.LogWarningFormat("Component type {0} not set in UI object {1} ",
                        componentType.ToString(), gameObject.name.ToString());
                    return;
                }
                ColorBlock sc = sliderComponent.colors;
                sc.normalColor = colorTheme.sliderColor;
                sliderComponent.colors = sc;
                break;

            case (ComponentType.DROPDOWN):
                if (dropdownComponent == null)
                {
                    Debug.LogWarningFormat("Component type {0} not set in UI object {1} ",
                        componentType.ToString(), gameObject.name.ToString());
                    return;
                }
                ColorBlock dc = dropdownComponent.colors;
                dc.normalColor = colorTheme.dropdownColor;
                dropdownComponent.colors = dc;
                break;

            case (ComponentType.IMAGE):
                if (imageComponent == null)
                {
                    Debug.LogWarningFormat("Component type {0} not set in UI object {1} ",
                        componentType.ToString(), gameObject.name.ToString());
                    return;
                }
                imageComponent.color = colorTheme.imageColor;
                break;
        }
    }

    private void OnDisable()
    {
        SettingsController.Instance.ColorThemeChange.RemoveListener(SetColorByTheme);
    }
}
