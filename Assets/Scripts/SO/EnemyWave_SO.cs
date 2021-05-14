
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyWaveAsset", menuName = "EnemyWave")]
public class EnemyWave_SO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    public int NumberOfEnemies
    {
        get { return enemyPrefabs.Count; }
    }
    public GameObject EnemyInWave(int i)
    {
        return enemyPrefabs[i];
    }
}

