using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
	[SerializeField] private string levelToLoad;
	[SerializeField] private SceneFader sceneFader;

	private void Start()
	{
		if (levelToLoad == "") { levelToLoad = "LevelSelect"; }
	}

	public void Play()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit()
	{
		Debug.Log("gusbai");
		Application.Quit();
	}
}