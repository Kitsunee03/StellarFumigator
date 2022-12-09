using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
	private int enemiesAlive = 0;
	[SerializeField] private List<Wave> waves;

	[SerializeField] private float timeBetweenWaves = 15f;
	[SerializeField] private float countdown = 10f;

	[SerializeField] private GameManager gameManager;
	private int waveIndex = 0;

	private void Update()
	{
		//Next Wave timer
		if (enemiesAlive == 0 && waveIndex != 0) { countdown -= Time.deltaTime * 2; }
		else { countdown -= Time.deltaTime; }
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		//Succesfull survival
		if (waveIndex == waves.Count && enemiesAlive == 0)
		{
			gameManager.WinLevel();
			enabled = false;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}
	}

	private IEnumerator SpawnWave()
	{
		GameStats.Rounds--;

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
	public float NextWaveTime { get { return countdown; } private set { countdown = value; } }
	public int WavesLeft { get { return waves.Count - waveIndex; } }
	public int EnemiesAlive { get { return enemiesAlive; } set { enemiesAlive = value; } }
	#endregion
}