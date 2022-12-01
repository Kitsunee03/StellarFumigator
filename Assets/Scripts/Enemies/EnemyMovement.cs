using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Vector3 m_corePosition;
    private Vector3 m_targetDestination;

    private Enemy enemyScript;
    private NavMeshAgent m_agent;

    [SerializeField] private float m_newDestinationMinStep;
    private float newPointRange;
    private float autoPathTime;
    private bool isAutoPathing;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        m_agent = GetComponent<NavMeshAgent>();

        newPointRange = Mathf.Clamp(m_agent.height * 2, 4f, 10f);
        m_corePosition = GameObject.FindGameObjectWithTag("Core").gameObject.transform.position;

        //First destination
        if (m_newDestinationMinStep == 0f) { m_newDestinationMinStep = 1.5f; }
        m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
        m_agent.SetDestination(m_targetDestination);
    }

    void Update()
    {
        m_agent.speed = enemyScript.Speed;

        //Point Reached
        if (Vector3.Distance(transform.position, m_targetDestination) <= 0.8f)
        {
            m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
            m_agent.SetDestination(m_targetDestination);
        }

        //Auto-Path
        if (isAutoPathing)
        {
            m_agent.SetDestination(m_corePosition);
            autoPathTime += Time.deltaTime;
        }
        if (autoPathTime > 2f) {
            isAutoPathing = false;
            autoPathTime = 0f;
            m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
            m_agent.SetDestination(m_targetDestination);
        }

        enemyScript.Speed = enemyScript.StartSpeed;
    }

    private Vector3 GetRandomNavMeshPoint(Vector3 center, float range)
    {
        Vector3 result = Vector3.zero;
        int iterations = 30;

        while (result == Vector3.zero && iterations > 0)
        {
            Vector3 randomPoint = center + Random.onUnitSphere * range;
            NavMeshHit hit;
            //Get random point
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                //Is new point nearer to target?
                float newPointDist = Vector3.Distance(hit.position, m_corePosition);
                float currentDist = Vector3.Distance(transform.position, m_corePosition);
                if (newPointDist < currentDist - m_newDestinationMinStep)
                {
                    Debug.DrawRay(hit.position, Vector3.up, Color.blue, 1.0f);
                    result = hit.position;
                    return result;
                }
            }
            iterations--;
        }

        //Auto-Path
        isAutoPathing = true;
        return m_corePosition;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 4f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 1.5f);
    }*/

    void EndPath()
    {
        GameStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}