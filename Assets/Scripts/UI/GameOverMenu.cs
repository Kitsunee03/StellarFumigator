using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private SceneFader sceneFader;
    [SerializeField] private string sceneToFade= "MainMenu";

    private void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();
    }

    public void Retry() { sceneFader.FadeTo(SceneManager.GetActiveScene().name); }
    public void Menu() { sceneFader.FadeTo(sceneToFade); }
}