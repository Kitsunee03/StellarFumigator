using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField] private string levelToLoad = "LevelSelect";
	private SceneFader sceneFader;

	[SerializeField] private GameObject menuTurret;

	[Header("Canvas")]
	[SerializeField] private GameObject optionsCanvas;
	[SerializeField] private GameObject creditsCanvas;
	[Header("Options Panels")]
	[SerializeField] private GameObject optionsPanel;
	[SerializeField] private GameObject volumePanel;
	[SerializeField] private GameObject controlsPanel;

	[Header("Buttons to disable")]
	[SerializeField] private List<Button> menuButtons;

	private bool isOptionsOpen;
	private bool isCreditsOpen;

	private void Start()
	{
		sceneFader = FindObjectOfType<SceneFader>();
	}

	public void Play()
	{
		for (int i = 0; i < menuButtons.Count; i++) { menuButtons[i].interactable = false; }
		sceneFader.FadeTo(levelToLoad);
	}

	public void OptionsToggle()
	{
		isOptionsOpen = !isOptionsOpen;
		if (isOptionsOpen)
		{
			//Enable and disable objects
			menuTurret.SetActive(false); 
			optionsCanvas.SetActive(true);
			optionsPanel.SetActive(true);
			volumePanel.SetActive(false);
			controlsPanel.SetActive(false);

			//Close Credits if opened
			if (isCreditsOpen) { creditsCanvas.SetActive(false); isCreditsOpen = false; }
		}
		else { menuTurret.SetActive(true); optionsCanvas.SetActive(false); }
	}
    public void OpenVolume()
    {
        optionsPanel.SetActive(false);
        volumePanel.SetActive(true);
    }
    public void OpenControls()
	{
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(true);
	}

    public void CreditsToogle()
	{
		isCreditsOpen = !isCreditsOpen;
		if (isCreditsOpen)
		{
			menuTurret.SetActive(false); 
			creditsCanvas.SetActive(true);

			//Close Options if opened
			if (isOptionsOpen) { optionsCanvas.SetActive(false); isOptionsOpen = false; }
		}
		else { menuTurret.SetActive(true); creditsCanvas.SetActive(false); }
	}

	public void GoBack()
	{
		volumePanel.SetActive(false);
		controlsPanel.SetActive(false);
		optionsPanel.SetActive(true);
	}

	public void Quit()
	{
		Debug.Log("gusbai");
		Application.Quit();
	}
}