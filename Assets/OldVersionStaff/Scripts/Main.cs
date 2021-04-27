﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

    public static int enemyKilled = 0;
    public static int projectilesLunched = 0;
    public static int enemyHit = 0;
    public static float accuracy = 0f;

    [Header("Set in Inspector")]
    public float gameRestartDelay = 2f;
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f; //отступ для позиционирования

    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] { WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield };

    private BoundsCheck bndCheck;

    public void ShipDestroyed(Enemy e)
    {
        // Сгенерировать бонус с заданной вероятностью
        if (Random.value <= e.powerUpDropChance)
        {
            enemyKilled++;
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);
            pu.transform.position = e.transform.position;
        }
    }

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        //float xMin = -bndCheck.camWidth + enemyPadding;
        //float xMax = bndCheck.camWidth - enemyPadding;
        //pos.x = Random.Range(xMin, xMax);
        //pos.y = bndCheck.camHeights + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay) //Сущность нужна чтобы учесть задержку при смене сцены
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_1");
        enemyKilled = 0;
    }

    /// <summary>
    /// Статическая функция, возвращающая WeaponDefinition из статического защищенного поля WEAP_DICT класса Main.
    /// </summary>
    /// <returns>
    /// Экземпляр WeaponDefinition или, если нет такого определения для указанного WeaponType, возвращает новый экземпляр WeaponDefinition с типом none.
    /// </returns>
    /// <param name="wt">
    /// Тип оружия WeaponType, для которого требуется получить WeaponDefinition
    /// </param>
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }

/*    public void FixedUpdate()
    {
        if (projectilesLunched != 0)
        {
            accuracy = (float)enemyHit / (float)projectilesLunched;
        }
    }*/
}
