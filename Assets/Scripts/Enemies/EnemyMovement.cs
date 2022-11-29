using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemyScript;
	private NavMeshAgent agent;

	void Start()
	{
		enemyScript = GetComponent<Enemy>();
		agent = GetComponent<NavMeshAgent>();

		target = Waypoints.Points[wavepointIndex];
	}

	void Update()
	{
		//Move Mob
		//Vector3 dir = target.position - transform.position;
		//transform.Translate(dir.normalized * enemyScript.Speed * Time.deltaTime, Space.World);

		agent.speed = enemyScript.Speed;
		agent.SetDestination(target.position);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

		enemyScript.Speed = enemyScript.StartSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.Points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.Points[wavepointIndex];
	}

	void EndPath()
	{
		GameStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}