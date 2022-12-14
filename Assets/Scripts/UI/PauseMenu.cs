using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseCanvas;
	[SerializeField] private SceneFader sceneFader;

    [Header("Options Panels")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject volumePanel;

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
        if (pauseCanvas.activeSelf) 
		{
			Time.timeScale = 0f; 
			panel.SetActive(true);
            pausePanel.SetActive(true);
			volumePanel.SetActive(false);

        }
		else 
		{ 
			Time.timeScale = 1f; 
			panel.SetActive(false);
            pausePanel.SetActive(false);
            volumePanel.SetActive(false);
        }
	}

	public void Retry()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void OpenVolume()
	{
		pausePanel.SetActive(false);
		volumePanel.SetActive(true);
	}
	public void GoBack()
	{
        volumePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Menu()
	{
		Toggle();
		sceneFader.FadeTo("MainMenu");
	}
}