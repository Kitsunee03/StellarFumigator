using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool isTutorial;
    private SceneFader fader;

    private float m_sceneResetTimer = 3f;
    private static bool gameIsOver;
	private string sceneToFade = "MainMenu";


    private void Start()
	{
        fader = FindObjectOfType<SceneFader>();

        gameIsOver = false;
	}

	private void Update()
	{
		if (gameIsOver)
		{
			m_sceneResetTimer -= Time.deltaTime;
			if (m_sceneResetTimer <= 0f) { fader.FadeTo(sceneToFade); }

			return;
		}


		if (isTutorial) { return; }
		//Defeated
		if (GameStats.CoreHealth <= 0) { EndGame(); }
	}

	private void EndGame()
	{
		gameIsOver = true;
	}

	public static void WinLevel()
	{
		gameIsOver = true;
	}
}