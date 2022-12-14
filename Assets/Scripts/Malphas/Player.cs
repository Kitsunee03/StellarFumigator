using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private CharacterController m_controller;
    private Camera m_camera;
    private AudioSource m_audioSource;

    [Header("Managers")]
    private InputManager m_input;

    [Header("Movement")]
    private Vector3 m_direction;
    private Vector3 m_finalVelocity;
    private float m_speed;
    private float m_maxSpeed;
    [Header("Rotation")]
    private float m_turnSmoothTime;
    private float m_turnSmoothSpeed;
    [Header("Jump and Fall")]
    private float m_gravity;
    private float m_jumpForce;
    private float m_collRadius;

    [Header("Gameplay Management")]
    private bool m_isDying;
    private SceneFader fader;
    private float m_sceneResetTimer = 3f;

    [Header("Malphas Modes")]
    [SerializeField] private Color[] m_colors;
    [SerializeField] private List<Material> m_materialsToPaint;
    private PLAYER_MODE m_currentMode;

    [Header("Weapon Mode")]
    [SerializeField] private GameObject m_bulletPrfb;
    [SerializeField] Transform m_attackSpawnPos;
    private bool canAttack;
    private float attackCooldown;

    private bool isAttacking;
    private float attackingTime;

    private bool startAttackCooldown;
    private float attackTimer;

    [Header("Dash")]
    private Vector3 dashDirection;
    private float dashingPower;
    private bool canDash = true;
    private float dashingCooldown;

    private bool isDashing;
    private float dashingTime;

    private bool startDashCooldown;
    private float dashTimer;

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        m_audioSource = GetComponent<AudioSource>();
        m_camera = Camera.main;
    }
    private void Start()
    {
        m_input = InputManager._INPUT_MANAGER;
        fader = FindObjectOfType<SceneFader>();
        canAttack = true;

        m_currentMode = PLAYER_MODE.WEAPON;
        ChangeMalphasColor(m_colors[(int)m_currentMode]);

        //Default Values: Movement
        m_finalVelocity = Vector3.zero;
        if (m_maxSpeed == 0f) { m_maxSpeed = 8f; }

        //Default Values: Jump and Fall
        if (m_gravity == 0f) { m_gravity = 20f; }
        if (m_jumpForce == 0f) { m_jumpForce = 8f; }
        m_collRadius = m_controller.radius;

        //Default Values: Rotation
        if (m_turnSmoothTime == 0f) { m_turnSmoothTime = 0.1f; }
        if (m_turnSmoothSpeed == 0f) { m_turnSmoothSpeed = 3f; }

        //Default Values: Dash
        if (dashingPower == 0f) { dashingPower = 10f; }
        if (dashingTime == 0f) { dashingTime = 0.3f; }
        if (dashingCooldown == 0f) { dashingCooldown = 2f; }
        dashTimer = dashingCooldown;

        //Default Values: Attack
        if (attackingTime == 0f) { attackingTime = 0.8f; }
        if (attackCooldown == 0f) { attackCooldown = 1f; }
        attackTimer = attackCooldown;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) { return; }
        if (GameManager.gameIsOver)
        {
            //Player Defeated
            isDashing = false;
            m_isDying = true;
            Gravity();
            GetMovement();
            Move();
            return;
        }

        ChangeMode();
        switch (m_currentMode)
        {
            default: { break; }
            case PLAYER_MODE.WEAPON:
                {
                    Jump();
                    Dash();
                    Shoot();
                    break;
                }
            case PLAYER_MODE.ARCHITECT:
                {
                    break;
                }
            case PLAYER_MODE.MINER:
                {
                    CurrentMode = PLAYER_MODE.WEAPON;
                    ChangeMalphasColor(m_colors[(int)m_currentMode]);
                    break;
                }
        }

        //Base Movement
        Gravity();
        GetMovement();
        PlayerRotation();
        Move();

        //Cooldowns
        AttackCooldown();
        DashCooldown();
    }

    private void GetMovement()
    {
        if (isDashing) { return; }

        //GetAxis: calcular velocidad XZ
        if (!m_isDying)
        {
            m_direction = Quaternion.Euler(0f, m_camera.transform.eulerAngles.y, 0f)
                * new Vector3(m_input.GetMovementAxis().x, m_direction.y, m_input.GetMovementAxis().y);
        }

        //Save direction
        if (IsMoving()) { dashDirection = new Vector3(m_direction.x, 0f, m_direction.z).normalized; }

        m_direction.y = -1f;
        m_direction.Normalize();

        //Acceleration
        if (IsMoving() && m_speed < m_maxSpeed) { m_speed += 6f * Time.deltaTime; }
        else if (!IsMoving() && m_speed > 0f) { m_speed = 0f; }
        if (m_speed > m_maxSpeed) { m_speed = m_maxSpeed; }

        //Velocidad final XZ
        m_finalVelocity.x = m_direction.x * m_speed;
        m_finalVelocity.z = m_direction.z * m_speed;
    }

    private void PlayerRotation()
    {
        //Rotation
        if (CurrentMode == PLAYER_MODE.WEAPON && !isDashing)
        {
            Vector3 WorldPoint = Utils.GetMouseWorldPosition();
            Vector3 difference = WorldPoint - transform.position;
            difference.Normalize();

            float targetRotation = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref m_turnSmoothSpeed, m_turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            float targetRotation;
            if (IsDashing) { targetRotation = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg; }
            else { targetRotation = Mathf.Atan2(m_direction.x, m_direction.z) * Mathf.Rad2Deg; }

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref m_turnSmoothSpeed, m_turnSmoothTime);
            if (IsMoving()) { transform.rotation = Quaternion.Euler(0f, angle, 0f); }
        }
    }
    private void Move()
    {
        //Move
        m_controller.Move(m_finalVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (IsGrounded() && m_input.GetJumpButtonPressed())
        {
            Impulse(1);
        }
    }
    private void Gravity()
    {
        if (isDashing) { return; }

        if (IsGrounded())
        {
            if (!m_input.GetJumpButtonPressed())
            {
                m_finalVelocity.y = m_direction.y * m_gravity * Time.deltaTime;
            }
        }
        else { m_finalVelocity.y += m_direction.y * m_gravity * Time.deltaTime; }
    }

    private void Dash()
    {
        if (m_input.GetDashButtonPressed() && canDash)
        {
            //Start Dashing
            canDash = false;
            isDashing = true;
            dashTimer = 0f;
            startDashCooldown = true;

            m_finalVelocity += dashDirection * dashingPower;
            m_finalVelocity.y = 0f;
        }
    }
    private void DashCooldown()
    {
        if (startDashCooldown)
        {
            dashTimer += Time.deltaTime;
            dashTimer = Mathf.Clamp(dashTimer, 0f, dashingCooldown);

            if (dashTimer >= dashingTime) { isDashing = false; }
            if (dashTimer >= dashingCooldown) { canDash = true; startDashCooldown = false; }
        }
    }

    private void Shoot()
    {
        if (m_input.GetShootButtonPressed() && Utils.GetMouseWorldPosition() != Vector3.zero && canAttack && !isDashing)
        {
            //Start Attacking
            canAttack = false;
            isAttacking = true;
            attackTimer = 0f;
            startAttackCooldown = true;

            //SFX
            int randNum = Random.Range(0, 2);
            if (randNum == 0) { m_audioSource.PlayOneShot(SoundManager.Instance.GetSound((int)SOUNDS.PLAYER_ATK_1)); }
            else { m_audioSource.PlayOneShot(SoundManager.Instance.GetSound((int)SOUNDS.PLAYER_ATK_2)); }

            //Projectile Shoot
            Vector3 aimDir = (Utils.GetMouseWorldPosition() - m_attackSpawnPos.position).normalized;
            GameObject bulletPrfb = Instantiate(m_bulletPrfb, m_attackSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            Bullet bullet = bulletPrfb.GetComponent<Bullet>();

            //Try Set enemy as Target (aim)
            Enemy enemy = Utils.GetMousePointingObject().GetComponent<Enemy>();
            if (enemy != null && bullet != null) { bullet.SetTarget(enemy.transform); }
            //Try Set tutorial enemy as Target (aim)
            else
            {
                TutorialEnemy tutoEnemy = Utils.GetMousePointingObject().GetComponent<TutorialEnemy>();
                if (tutoEnemy != null && bullet != null) { bullet.SetTarget(tutoEnemy.transform); }
            }
        }
    }
    private void AttackCooldown()
    {
        if (startAttackCooldown)
        {
            attackTimer += Time.deltaTime;
            attackTimer = Mathf.Clamp(attackTimer, 0f, attackCooldown);

            if (attackTimer >= attackingTime) { isAttacking = false; }
            if (attackTimer >= attackCooldown) { canAttack = true; startAttackCooldown = false; }
        }
    }

    private bool IsGrounded()
    {
        if (m_finalVelocity.y >= 0) { return false; }
        RaycastHit hit;
        int groundedRay = 0;
        Vector3 posMinusY = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        if (Physics.Linecast(transform.position, posMinusY, out hit, ~LayerMask.GetMask("Player")))
        {
            groundedRay++;
        }

        if (Physics.Linecast(transform.position + transform.forward * m_collRadius,
            posMinusY + transform.forward * m_collRadius, out hit, ~LayerMask.GetMask("Player")))
        {
            groundedRay++;
        }
        if (Physics.Linecast(transform.position - transform.forward * m_collRadius,
            posMinusY - transform.forward * m_collRadius, out hit, ~LayerMask.GetMask("Player")))
        {
            groundedRay++;
        }


        if (Physics.Linecast(transform.position + transform.right * m_collRadius,
            posMinusY + transform.right * m_collRadius, out hit, ~LayerMask.GetMask("Player")))
        {
            groundedRay++;
        }
        if (Physics.Linecast(transform.position - transform.right * m_collRadius,
            posMinusY - transform.right * m_collRadius, out hit, ~LayerMask.GetMask("Player")))
        {
            groundedRay++;
        }

        return groundedRay > 0 || m_controller.isGrounded;
    }

    /*private void OnDrawGizmos()
    {
        Vector3 posMinusY = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, posMinusY);

        Gizmos.DrawLine(transform.position + transform.forward * m_collRadius, posMinusY + transform.forward * m_collRadius);
        Gizmos.DrawLine(transform.position - transform.forward * m_collRadius, posMinusY - transform.forward * m_collRadius);
        Gizmos.DrawLine(transform.position + transform.right * m_collRadius, posMinusY + transform.right * m_collRadius);
        Gizmos.DrawLine(transform.position - transform.right * m_collRadius, posMinusY - transform.right * m_collRadius);
    }*/

    private void ResizeCollider(float p_heightMultiplier, float p_centerPos)
    {
        m_controller.height = p_heightMultiplier;
        m_controller.center = Vector3.up * p_centerPos;
    }

    public void Impulse(float p_impulsePower)
    {
        m_finalVelocity.y = m_jumpForce * p_impulsePower;
    }

    private void ChangeMode()
    {
        if (m_input.GetModeSelectorButtonPressed())
        {
            if (m_currentMode == PLAYER_MODE.LAST_NO_USE - 1) { m_currentMode = 0; }
            else { m_currentMode++; }

            ChangeMalphasColor(m_colors[(int)m_currentMode]);
        }
    }

    private void ChangeMalphasColor(Color color)
    {
        for(int i = 0; i < m_materialsToPaint.Count; i++)
        {
            m_materialsToPaint[i].color = color;
        }
    }

    #region Accessors
    public bool IsMoving() { return new Vector2(m_input.GetMovementAxis().x, m_input.GetMovementAxis().y).magnitude > 0.1f; }
    public Vector3 GetMovementDir() { return m_direction; }
    public bool IsDying { get { return m_isDying; } set { m_isDying = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
    public bool CanAttack { get { return canAttack; } set { canAttack = value; } }
    public PLAYER_MODE CurrentMode { get { return m_currentMode; } set { m_currentMode = value; } }
    public float GetDashCooldown { get { return dashTimer / dashingCooldown; } }
    public float GetAttackCooldown { get { return attackTimer / attackCooldown; } }
    #endregion
}