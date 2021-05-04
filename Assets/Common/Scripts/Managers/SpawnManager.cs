using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject explosionPrefab;

    public void CreateExplosionFX (Vector3 position)
    {
        GameObject explosion =  Instantiate<GameObject>(explosionPrefab,gameObject.transform);
        Debug.Log("Explosion");
        explosion.transform.position = position;
        explosion.SetActive(true);
    }

}
