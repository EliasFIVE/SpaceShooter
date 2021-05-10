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

    [Space(10)]
    [Header("Enemy spawn settings")]
    [SerializeField] private float enemyDefaultPadding = 3f;


    private BoundsCheck bound;
    private ScreenBorders borders;


    protected virtual void Start()
    {
        bound = gameObject.GetComponent<BoundsCheck>();
        borders = ScreenBorders.Instance;
    }

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

    public void CreateEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate<GameObject>(enemyPrefab);
        enemy.SetActive(false);
        //Set Enemy position
        float enemyPadding = enemyDefaultPadding;
        if (enemy.GetComponent<BoundsCheck>() != null && enemy.GetComponent<BoundsCheck>().radius != 0)
        {
            enemyPadding = Mathf.Abs(enemy.GetComponent<BoundsCheck>().radius);
        }
        Vector3 position = Vector3.zero;
        float xMin = -borders.CamWidth + enemyPadding;
        float xMax = borders.CamWidth - enemyPadding;
        position.x = Random.Range(xMin, xMax);
        position.y = borders.CamHeight + enemyPadding;
        position.z = 0;
        enemy.transform.position = position;
        enemy.SetActive(true);
    }
}
