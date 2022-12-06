using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	[Header("Enemy Stats")]
	[SerializeField] private float startSpeed = 10f;
    [SerializeField] private float startHealth = 100f;
    [SerializeField] private int worth = 5;
    [SerializeField] private GameObject deathEffect;
    private float speed;
	private float health;

	[Header("Enemy Stuff")]
	private Transform mainCamera;
	[SerializeField] private Canvas healthCanvas;
	[SerializeField] private Image healthBar;
	private bool isDead = false;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
		mainCamera = FindObjectOfType<CameraMovement>().gameObject.transform;

    }
	private void Update()
	{
		healthCanvas.transform.LookAt(mainCamera);
    }

	public void TakeDamage(float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	public void Slow(float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	void Die()
	{
		isDead = true;

		GameStats.Gems += worth;

		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}

	#region Accessors
	public float Speed { get { return speed; } set { speed = value; } }
	public float StartSpeed { get { return startSpeed; } set { startSpeed = value; } }
	#endregion
}