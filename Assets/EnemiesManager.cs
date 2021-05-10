using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    public List<EnemyWave_SO> EnemyWaves;
    [SerializeField] float timeDelayBetweenShipSpawn = 2f;

    private int activeWaveIndex;
    private int activeEnemies;


    private void Start()
    {
        activeWaveIndex = 0;
        SpawnActiveWave();
    }
    public void SpawnWaveByNumber(int waveIndex)
    {
        activeWaveIndex = waveIndex;
        EnemyWave_SO activeWave = EnemyWaves[waveIndex];
        activeEnemies = activeWave.NumberOfEnemies;

        StartCoroutine(SpawnEnemyWaveCoroutine(activeWave,timeDelayBetweenShipSpawn));
    }

    public void SpawnActiveWave()
    {
        SpawnWaveByNumber(activeWaveIndex);
    }

    public void SpawnNextWave()
    {
        activeWaveIndex++;
        SpawnActiveWave();
    }

    IEnumerator SpawnEnemyWaveCoroutine(EnemyWave_SO wave, float timeDelay)
    {
        for(int i= 0; i<wave.NumberOfEnemies; i++)
        {
            yield return new WaitForSeconds(timeDelay);
            SpawnManager.Instance.CreateEnemy(wave.EnemyInWave(i));
        }
    }

    public void OnEmemyDeath()
    {
        activeEnemies -= 1; ;
        Debug.LogFormat("Enemy killed. Active enemies left {0}", activeEnemies);
    }
}
