using UnityEngine;

public class GameManager : MonoBehaviour
{
	private WaveSpawner waveSpawner;
    private SceneFader fader;

    private float m_sceneResetTimer = 3f;
    private static bool gameIsOver;

	private void Start()
	{
        waveSpawner = FindObjectOfType<WaveSpawner>();
        fader = FindObjectOfType<SceneFader>();

        gameIsOver = false;
	}

	private void Update()
	{
		if (gameIsOver)
		{
			m_sceneResetTimer -= Time.deltaTime;
			if (m_sceneResetTimer <= 0f) { fader.FadeTo("MainMenu"); }

			return;
		}

		//Level survived
		if (waveSpawner.WavesLeft == 0 && waveSpawner.EnemiesAlive == 0)
		{
			WinLevel();
		}

		//F
		if (GameStats.CoreHealth <= 0)
		{
			EndGame();
		}
	}

	private void EndGame()
	{
		gameIsOver = true;
	}

	private void WinLevel()
	{
		gameIsOver = true;
	}

    #region Accessors

    #endregion
}