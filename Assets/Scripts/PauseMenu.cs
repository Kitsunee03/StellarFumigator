using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseCanvas;
	[SerializeField] private SceneFader sceneFader;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		pauseCanvas.SetActive(!pauseCanvas.activeSelf);

		//Stop-Resume game
		if (pauseCanvas.activeSelf) { Time.timeScale = 0f; }
		else { Time.timeScale = 1f; }
	}

	public void Retry()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		Toggle();
		sceneFader.FadeTo("MainMenu");
	}
}