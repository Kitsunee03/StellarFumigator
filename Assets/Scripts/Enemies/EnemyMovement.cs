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
    private Animator m_animator;
    private WaveSpawner waveSpawner;

    [SerializeField] private int damageToCore = 1;
    [SerializeField] private float m_pointReachedDistance = 0.8f;
    [SerializeField] private float m_newDestinationMinStep;
    private float newPointRange;
    private float autoPathTime;
    private bool isAutoPathing;

    [Header("RichardMode")]
    private bool pathPaused;
    private Vector3 lastAgentVelocity;
    private Vector3 lastAgentPath;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();

        newPointRange = Mathf.Clamp(m_agent.height * 2, 4f, 10f);
        m_corePosition = GameObject.FindGameObjectWithTag("Core").gameObject.transform.position;
        waveSpawner = FindObjectOfType<WaveSpawner>();

        //First destination
        if (m_newDestinationMinStep == 0f) { m_newDestinationMinStep = 1.5f; }
        m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
        m_agent.SetDestination(m_targetDestination);
        m_animator.SetBool("isWalking", true);
    }

    void Update()
    {
        if (Time.timeScale == 0f) { return; }
        if (GameManager.gameIsOver)
        {
            m_agent.SetDestination(transform.position);
            m_animator.SetBool("isWalking", false);
            m_animator.SetBool("isRolling", false);
            return;
        }

        //Richard Mode
        if (GameManager.RichardMode && !pathPaused) { pause(); pathPaused = true; }
        if (!GameManager.RichardMode && pathPaused) { resume(); pathPaused = false; }

        if (!pathPaused)
        {
            m_agent.speed = enemyScript.Speed;

            //Point Reached
            if (Vector3.Distance(transform.position, m_targetDestination) <= m_pointReachedDistance)
            {
                m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
                m_agent.SetDestination(m_targetDestination);
            }

            //Auto-Path
            if (isAutoPathing)
            {
                m_agent.SetDestination(m_corePosition);
                autoPathTime += Time.deltaTime;
                //Animator
                m_animator.SetBool("isRolling", true);
                m_animator.SetBool("isWalking", false);
            }
            //Stop Autopathing
            if (autoPathTime > 2f)
            {
                isAutoPathing = false;
                autoPathTime = 0f;
                m_targetDestination = GetRandomNavMeshPoint(transform.position, newPointRange);
                m_agent.SetDestination(m_targetDestination);
                //Animator
                m_animator.SetBool("isRolling", false);
                m_animator.SetBool("isWalking", true);
            }

            //End path
            if (Vector3.Distance(transform.position, m_corePosition) < 1.4f) { EndPath(); }

            enemyScript.Speed = enemyScript.StartSpeed;
        }
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

    private void EndPath()
    {
        GameStats.CoreHealth -= damageToCore;
        waveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void pause()
    {
        lastAgentVelocity = m_agent.velocity;
        lastAgentPath = m_targetDestination;
        
        m_agent.velocity = Vector3.zero;
        m_agent.ResetPath();

        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isRolling", false);
    }

    private void resume()
    {
        m_agent.velocity = lastAgentVelocity;
        m_agent.SetDestination(lastAgentPath);
        if (isAutoPathing) { m_animator.SetBool("isRolling", true); }
        else { m_animator.SetBool("isWalking", true); }
    }
}