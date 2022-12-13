using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseCanvas;
	[SerializeField] private SceneFader sceneFader;

	private void OnEnable()
	{
        pauseCanvas.SetActive(false);
        InputManager._INPUT_MANAGER.SetPauseButtonPressedTime(0.01f);		
	}

	private void Update()
	{
		if (InputManager._INPUT_MANAGER.GetPauseButtonPressed())
		{
			InputManager._INPUT_MANAGER.SetPauseButtonPressedTime(0.01f);
			Toggle();
		}
	}

	public void Toggle()
	{
		InputManager._INPUT_MANAGER.SetShootButtonPressedTime(0.01f);

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