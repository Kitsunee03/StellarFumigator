using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform targetEnemy;

	[SerializeField] float speed = 70f;
	[SerializeField] int damage = 50;
	[SerializeField] float explosionRadius = 0f;
	[SerializeField] bool isPlayerBullet;

	[SerializeField] GameObject impactEffect;

	private void Update()
	{
		if (targetEnemy == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = targetEnemy.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(targetEnemy);
	}

	private void HitTarget(Transform target = null)
	{
		GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else if (target != null)
		{
			DamageEnemy(target);
		}

		Destroy(gameObject);
	}

	private void Explode()
	{
		//Get near enemies and damage them
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				DamageEnemy(collider.transform);
			}
		}
	}

	private void DamageEnemy(Transform enemy)
	{
		//Get the enemy and apply damage
		Enemy enemyScript = enemy.GetComponent<Enemy>();

		if (enemyScript != null)
		{
			enemyScript.TakeDamage(damage);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isPlayerBullet && other.gameObject.CompareTag("Turret")) { return; }

		//Collieds an enemy
		if (other.gameObject.CompareTag("Enemy"))
		{
			HitTarget(other.transform);
		}
		//Collides others
		else
		{
			HitTarget();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

	public void SetTarget(Transform p_target)
	{
		targetEnemy = p_target;
	}
}