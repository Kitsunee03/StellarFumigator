using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	[Header("Enemy Stats")]
	[SerializeField] private float startSpeed;
    [SerializeField] private float startHealth;
    [SerializeField] private int worth;
    [SerializeField] private GameObject deathEffect;
    private float speed;
	private float health;

	[Header("Enemy Stuff")]
    [SerializeField] private Canvas healthCanvas;
    [SerializeField] private Image healthBar;
    private Transform mainCamera;
	private WaveSpawner waveSpawner;
	private bool isDead = false;

	void Start()
	{
		//Default values
		if(worth == 0) { worth = 10; }
		if(startHealth == 0f) { startHealth = 100f;}
		if(startSpeed == 0f) { startSpeed = 10f;}

		speed = startSpeed;
		health = startHealth;
		mainCamera = FindObjectOfType<CameraMovement>().gameObject.transform;
		waveSpawner = FindObjectOfType<WaveSpawner>();

    }
	private void Update()
	{
		healthCanvas.transform.LookAt(mainCamera);
    }

	public void TakeDamage(float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead) { Die(); }
	}

	public void Slow(float pct) { speed = startSpeed * (1f - pct); }

	void Die()
	{
		isDead = true;

		GameStats.Gems += worth;

		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

        waveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}

	#region Accessors
	public float Speed { get { return speed; } set { speed = value; } }
	public float StartSpeed { get { return startSpeed; } set { startSpeed = value; } }
	#endregion
}