using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField] private string levelToLoad;
	[SerializeField] private SceneFader sceneFader;

	[SerializeField] private GameObject menuTurret;
	[SerializeField] private GameObject optionsCanvas;
	[SerializeField] private GameObject creditsCanvas;
	private bool isOptionsOpen;
	private bool isCreditsOpen;

	private void Start()
	{
		if (levelToLoad == "") { levelToLoad = "LevelSelect"; }
	}

	public void Play()
	{
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