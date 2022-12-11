using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    private int enemiesAlive = 0;
    [SerializeField] private List<Wave> waves;

    [SerializeField] private float firstWaveCountdown = 6f;
    private int waveIndex = 0;

    private void Update()
    {
        //Reset waves
        if (waveIndex == waves.Count && enemiesAlive == 0) { waveIndex = 0; }

        //Next Wave timer
        if (firstWaveCountdown > 0f) { firstWaveCountdown -= Time.deltaTime; }
        if (firstWaveCountdown <= 0f && enemiesAlive == 0) { StartCoroutine(SpawnWave()); }
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesAlive += wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int randSpawn = Random.Range(0, SpawnPoints.Spawns.Length);
        Instantiate(enemy, SpawnPoints.Spawns[randSpawn].position, SpawnPoints.Spawns[randSpawn].rotation);
    }

    #region Accessors
    public int EnemiesAlive { get { return enemiesAlive; } set { enemiesAlive = value; } }
    #endregion
}