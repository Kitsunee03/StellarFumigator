using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;
	public Wave[] waves;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public Text waveCountdownText;
	public GameManager gameManager;
	private int waveIndex = 0;

	private void Update()
	{
		if (EnemiesAlive > 0) { return; }

		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		if (waveIndex == waves.Length)
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

		//waveCountdownText.text = string.Format("{00:00.00}", countdown);
	}

	private IEnumerator SpawnWave()
	{
		GameStats.Rounds--;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.enemyCount;

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
}