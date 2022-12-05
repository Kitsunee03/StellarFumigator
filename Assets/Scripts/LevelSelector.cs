using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	[SerializeField] private SceneFader fader;
	[SerializeField] private Button[] levelButtons;

	public void Select(string levelName) { fader.FadeTo(levelName); }
}