using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationPerSecond = 0.1f;

    [Header("Set Dynamically")]
    //public int levelShown = 0;

    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float rZ = -(rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);

        //Not good to check this every frame
        /*        int currentLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
                if (levelShown != currentLevel)
                {
                    levelShown = currentLevel;
                }

        material.mainTextureOffset = new Vector2(0.2f * levelShown, 0); */
    }
}
