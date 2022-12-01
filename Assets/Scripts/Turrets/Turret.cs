using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]
	[SerializeField] float range = 15f;
	[SerializeField] Transform partToRotate;
	[SerializeField] float turnSpeed = 10f;
	[SerializeField] Transform firePoint;

	private string enemyTag = "Enemy";

	[Header("Bullets (default)")]
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float fireRate = 1f;
	private float fireCountdown = 0f;

	[SerializeField] int damageOverTime = 10;
	[SerializeField] float slowAmount = 0.5f;

	[Header("Laser Turret")]
    [SerializeField] bool useLaser = false;
    [SerializeField] LineRenderer lineRenderer;
	[SerializeField] ParticleSystem impactEffect;
	[SerializeField] Light impactLight;

	void Start()
	{
		//Update Target every 0.5 seconds
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	private void UpdateTarget()
	{
		//Get enemies
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		//Nearest enemy at range?
		if (nearestEnemy != null && shortestDistance <= range)
		{
			//Set as target
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else { target = null; }
	}

	void Update()
	{
		if (target == null)
		{
			//Disable Laser VFX
			if (useLaser && lineRenderer.enabled)
			{
				lineRenderer.enabled = false;
				impactEffect.Stop();
				impactLight.enabled = false;
			}

			return;
		}

		//Look At target
		LockOnTarget();

		//Shoot laser Beam
		if (useLaser) { Laser(); }
		//Shoot bullet
		else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser()
	{
		//Damage Enemy
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		//Laser VFX
		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;
		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot()
	{
		GameObject bulletPrfb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletPrfb.GetComponent<Bullet>();

		//Set Target
		if (bullet != null) { bullet.SetTarget(target); }
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}