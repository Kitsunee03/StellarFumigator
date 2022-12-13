using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("If true, can't lose")]
	[SerializeField] private bool isTutorial;

	[Header("UI and Menus")]
	private GameObject gameOverMenu;
	private GameObject pauseMenu;
	private GameObject playerUI;
	private SceneFader fader;

    private string sceneToFade = "LevelSelect";
    private float m_sceneResetTimer = 2f;
	private static bool gameWon;
	public static bool gameIsOver;
	public static bool RichardMode;

	private void Awake()
	{
		instance = this;	
	}

	private void Start()
	{
		fader = FindObjectOfType<SceneFader>();
        pauseMenu = FindObjectOfType<PauseMenu>().gameObject;

		if (isTutorial)
		{
			playerUI = FindObjectOfType<TutorialUIManager>().gameObject;
		}
		else
		{
			playerUI = FindObjectOfType<UIManager>().gameObject;
            gameOverMenu = FindObjectOfType<GameOverMenu>().gameObject;
			gameOverMenu.SetActive(false);

        }
       
		gameIsOver = false;
		gameWon = false;
	}

	private void Update()
	{
		if (gameWon)
		{
			m_sceneResetTimer -= Time.deltaTime;
			if (m_sceneResetTimer <= 0f) { fader.FadeTo(sceneToFade); }

			return;
		}

		//TESTING (DELETE OR COMMENT THIS)
		if (Input.GetKeyDown(KeyCode.K)) { GameStats.CoreHealth = 0; }
		if (Input.GetKeyDown(KeyCode.M)) { GameStats.Gems = 1000; }
		if (Input.GetKeyDown(KeyCode.L)) { RichardMode = !RichardMode; }

		if (isTutorial) { return; }
		//Defeated
		if (GameStats.CoreHealth <= 0) { LoseLevel(); }
	}

	private void LoseLevel()
	{
		pauseMenu.SetActive(false);
		playerUI.SetActive(false);
		gameOverMenu.SetActive(true);
		gameIsOver = true;
	}

	public void WinLevel()
	{
		gameWon = true;
        playerUI.SetActive(false);
        pauseMenu.SetActive(false);
    }
}