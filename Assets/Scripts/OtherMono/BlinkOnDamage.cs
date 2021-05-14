using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkOnDamage : MonoBehaviour, IDamagable
{

    private Color[] originalColors;
    private Material[] materials;
    private float showDamageDuration = 0.1f;
    private bool damageIsShown = false;

    private void Awake()
    {
        //Get materials and colors of this GO and childs
        materials = GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        BackUpColors();
    }

    private void BackUpColors()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }
    public void TakeDamage(int damage)
    {
        if (!damageIsShown)
        {
            ShowDamage();
            Invoke("UnShowDamage", showDamageDuration);
        }
    }

    void ShowDamage()
    {
        BackUpColors();
        damageIsShown = true;

        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        damageIsShown = false;
    }

    static public Material[] GetAllMaterials(GameObject go)
    {
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();
        List<Material> mats = new List<Material>();
        foreach (Renderer rend in rends)
        {
            mats.Add(rend.material);
        }
        return (mats.ToArray());
    }
}
