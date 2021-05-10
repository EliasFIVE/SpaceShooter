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
        StartActiveWave();
    }
    public void StartWaveByNumber(int waveIndex)
    {
        activeWaveIndex = waveIndex;
        EnemyWave_SO activeWave = EnemyWaves[waveIndex];
        activeEnemies = activeWave.NumberOfEnemies;

        StartCoroutine(SpawnEnemyWaveCoroutine(activeWave,timeDelayBetweenShipSpawn));
    }

    public void StartActiveWave()
    {
        StartWaveByNumber(activeWaveIndex);
    }

    public void StartNextWave()
    {
        activeWaveIndex++;
        StartActiveWave();
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

        if (activeEnemies == 0)
        {
            if (activeWaveIndex == (EnemyWaves.Count - 1))
            {
                OnAllWavesComplete();
                return;
            }

            StartNextWave();
        }
    }

    private void OnAllWavesComplete()
    {
        Debug.Log("All level waves complete");
        //Setup level win call to gamemanager
    }
}
