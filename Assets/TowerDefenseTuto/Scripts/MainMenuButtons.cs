using UnityEngine;

public class MainMenuButtons : MonoBehaviour {

	public string levelToLoad = "MainMenu";

	public SceneFader sceneFader;

	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit ()
	{
		Debug.Log("gusbai");
		Application.Quit();
	}

}