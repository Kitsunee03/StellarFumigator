using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
	[SerializeField] private Image blackScreen;
    [SerializeField] private AnimationCurve curve;

	private void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

	private IEnumerator FadeIn()
	{
		float time = 1f;

		while (time > 0f)
		{
			time -= Time.deltaTime;
			float curvE = curve.Evaluate(time);
			blackScreen.color = new Color(0f, 0f, 0f, curvE);
			yield return 0;
		}
	}

	private IEnumerator FadeOut(string scene)
	{
		float time = 0f;

		while (time < 1f)
		{
			time += Time.deltaTime;
			float curvE = curve.Evaluate(time);
			blackScreen.color = new Color(0f, 0f, 0f, curvE);
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}
}