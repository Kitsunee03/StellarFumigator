using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform target;

	[SerializeField] float speed = 70f;
    [SerializeField] int damage = 50;
    [SerializeField] float explosionRadius = 0f;

    [SerializeField] GameObject impactEffect;

	private void Update()
	{

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	private void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	private void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Damage(collider.transform);
			}
		}
	}

	private void Damage(Transform enemy)
	{
		Enemy enemyScript = enemy.GetComponent<Enemy>();

		if (enemyScript != null)
		{
			enemyScript.TakeDamage(damage);
		}
	}

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}