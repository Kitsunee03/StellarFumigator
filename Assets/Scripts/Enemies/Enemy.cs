using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float startSpeed = 10f;
	private float speed;

	[SerializeField] private float startHealth = 100;
	private float health;

	[SerializeField] private int worth = 50;

	[SerializeField] private GameObject deathEffect;

	[Header("Unity Stuff")]
	[SerializeField] Image healthBar;

	private bool isDead = false;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
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

		GameStats.m_money += worth;

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