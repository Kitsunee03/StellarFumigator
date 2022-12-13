using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField] private string levelToLoad = "LevelSelect";
	private SceneFader sceneFader;

	[SerializeField] private GameObject menuTurret;
	[SerializeField] private GameObject optionsCanvas;
	[SerializeField] private GameObject creditsCanvas;
	[SerializeField] private List<Button> menuButtons;
	private bool isOptionsOpen;
	private bool isCreditsOpen;

    private void Start()
    {
		sceneFader = FindObjectOfType<SceneFader>();

	}
    public void Play()
	{
		for(int i = 0; i < menuButtons.Count; i++) { menuButtons[i].interactable = false; }
		sceneFader.FadeTo(levelToLoad);
	}

    public void Options()
    {
		isOptionsOpen=!isOptionsOpen;
		if (isOptionsOpen) 
		{ 
			menuTurret.SetActive(false); optionsCanvas.SetActive(true);

            //Close Credits if opened
            if (isCreditsOpen) { creditsCanvas.SetActive(false); isCreditsOpen = false; }
        }
		else { menuTurret.SetActive(true); optionsCanvas.SetActive(false); }
    }
	public void Credits()
    {
        isCreditsOpen = !isCreditsOpen;
		if (isCreditsOpen) 
		{
			menuTurret.SetActive(false); creditsCanvas.SetActive(true);

            //Close Options if opened
            if (isOptionsOpen) { optionsCanvas.SetActive(false); isOptionsOpen = false; }
        }
		else { menuTurret.SetActive(true); creditsCanvas.SetActive(false); }
    }

    public void Quit()
	{
		Debug.Log("gusbai");
		Application.Quit();
	}
}