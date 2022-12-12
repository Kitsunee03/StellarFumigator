using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	[SerializeField] private SceneFader fader;
	[SerializeField] private Button[] levelButtons;

	public void GoToLevel(string levelName) { fader.FadeTo(levelName); }

	public void GoBack() { fader.FadeTo("MainMenu"); }
}