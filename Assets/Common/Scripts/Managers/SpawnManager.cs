using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Explosion spawn settings")]
    [SerializeField] GameObject explosionPrefab;

    [Space(10)]
    [Header("PowerUp item spawn settings")]
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] [Range(0, 1)] float powerUpChance;
    [Header("Set several equal items to increase chance of appiarence")]
    [SerializeField] List<Item_SO> items;


    public void CreateExplosionFX (Vector3 position)
    {
        GameObject explosion =  Instantiate<GameObject>(explosionPrefab,gameObject.transform);
        Debug.Log("Explosion");
        explosion.transform.position = position;
        explosion.SetActive(true);
    }

    public void CreatePowerUpItem(Vector3 position)
    {
        if (Random.value < powerUpChance)
        {
            GameObject powerUp = Instantiate<GameObject>(powerUpPrefab, gameObject.transform);
            Debug.Log("PowerUp Created");

            Item_SO item = items[Random.Range(0, items.Count)];
            powerUp.GetComponent<PowerUpController>().SetType(item);

            powerUp.transform.position = position;
            powerUp.SetActive(true);
        }
    }
}
