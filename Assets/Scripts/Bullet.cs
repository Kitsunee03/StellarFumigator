using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform targetEnemy;
	private Vector3 alternativeTarget;

	[SerializeField] private float speed = 70f;
	[SerializeField] private int damage = 50;
	[SerializeField] private float explosionRadius = 0f;
	[SerializeField] private bool isPlayerBullet;

	[SerializeField] private GameObject impactEffect;

	private void Start()
	{
		alternativeTarget = Utils.GetMouseWorldPosition();
		Destroy(gameObject, 10f);
	}
	private void Update()
	{
		Vector3 dir;

		//Turret Bullet without target
		if (targetEnemy == null && !isPlayerBullet)
		{
			Destroy(gameObject);
			return;
		}

		//Player Bullet without target
		else if (targetEnemy == null && isPlayerBullet) { dir = alternativeTarget - transform.position; }
		//Bullet with target
		else { dir = targetEnemy.position - transform.position; }

		float distanceThisFrame = speed * Time.deltaTime;

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(targetEnemy);
	}

	private void HitTarget(Transform target = null)
	{
		GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f) { Explode(); }
		else if (target != null) { DamageEnemy(target); }

		Destroy(gameObject);
	}

	private void Explode()
	{
		//Get near enemies and damage them
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy")) { DamageEnemy(collider.transform); }
		}
	}

	private void DamageEnemy(Transform enemy)
	{
		//Get the enemy and apply damage
		Enemy enemyScript = enemy.GetComponent<Enemy>();
		if (enemyScript != null) { enemyScript.TakeDamage(damage); }

		//Get the tutoriaEnemy and apply damage
		TutorialEnemy tutoEnemyScript = enemy.GetComponent<TutorialEnemy>();
		if (tutoEnemyScript != null) { tutoEnemyScript.TakeDamage(damage); }
	}

	private void OnTriggerEnter(Collider other)
	{
		//Avoid bullet-bounds collision
		if (other.gameObject.CompareTag("Wall")) { return; }
		//Avoid bullet-turret collision
		if (other.gameObject.CompareTag("Turret")) { return; }
		//Avoid playerBullet-player collision
		if (isPlayerBullet && other.gameObject.CompareTag("Player")) { return; }

		//Collieds an enemy
		if (other.gameObject.CompareTag("Enemy")) { HitTarget(other.transform); }
		//Collides others
		else { HitTarget(); }
	}

	/*void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}*/

	public void SetTarget(Transform p_target) { targetEnemy = p_target; }
}