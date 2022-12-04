using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private CharacterController m_controller;
    //private PlayerAnimator m_anim;
    private Camera m_camera;

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
    private float m_sceneResetTimer;

    [Header("Malphas Modes")]
    [SerializeField] private Color[] m_colors;
    private PLAYER_MODE m_currentMode;

    [Header("Weapon Mode")]
    [SerializeField] private GameObject m_bulletPrfb;
    [SerializeField] Transform m_attackSpawnPos;
    private bool canAttack;
    private bool isAttacking;
    private float attackingTime;
    private float attackCooldown;

    [Header("Dash")]
    private Vector3 dashDirection;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower;
    private float dashingTime;
    private float dashingCooldown;

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        //m_anim = GetComponent<PlayerAnimator>();
        m_camera = Camera.main;
    }
    private void Start()
    {
        m_input = InputManager._INPUT_MANAGER;
        canAttack = true;

        m_currentMode = PLAYER_MODE.WEAPON;
        //transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = m_colors[(int)m_currentMode];

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

        //Default Values: Attack
        if (attackingTime == 0f) { attackingTime = 0.8f; }
        if (attackCooldown == 0f) { attackCooldown = 1f; }
    }

    private void Update()
    {
        //Gameplay Management
        if (m_isDying) //Defeat
        {
            m_sceneResetTimer += Time.deltaTime;
            if (m_sceneResetTimer >= 3f) { SceneManager.LoadScene(0); }
        }
        else
        {
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
                        CurrentMode = PLAYER_MODE.WEAPON;                        break;
                    }
            }

            //Base Movement
            Gravity();
            GetMovement();
            PlayerRotation();
            Move();
        }
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
            StartCoroutine(DashCoroutine());
            m_finalVelocity += dashDirection * dashingPower;
            m_finalVelocity.y = 0f;
        }
    }
    private IEnumerator DashCoroutine()
    {
        if (!canDash) { yield break; }
        canDash = false;
        isDashing = true;

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Shoot()
    {
        if (m_input.GetShootButtonPressed() && Utils.GetMouseWorldPosition() != Vector3.zero && canAttack && !isDashing)
        {
            StartCoroutine(ShootCoroutine());
            //Projectile Shoot
            Vector3 aimDir = (Utils.GetMouseWorldPosition() - m_attackSpawnPos.position).normalized;
            GameObject bulletPrfb = Instantiate(m_bulletPrfb, m_attackSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            Bullet bullet = bulletPrfb.GetComponent<Bullet>();

            //Try Set Target (aim)
            Enemy enemy = Utils.GetMousePointingObject().GetComponent<Enemy>();
            if (enemy != null && bullet != null) { bullet.SetTarget(enemy.transform); }
        }
    }
    private IEnumerator ShootCoroutine()
    {
        canAttack = false;
        isAttacking = true;

        yield return new WaitForSeconds(attackingTime);
        isAttacking = false;
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private bool IsGrounded()
    {
        if (m_finalVelocity.y >= 0) { return false; }
        RaycastHit hit;
        float groundedRay = 0;
        Vector3 posMinusY = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        if (Physics.Linecast(transform.position, posMinusY, out hit))
        {
            groundedRay++;
        }

        if (Physics.Linecast(transform.position + transform.forward * m_collRadius,
            posMinusY + transform.forward * m_collRadius, out hit))
        {
            groundedRay++;
        }
        if (Physics.Linecast(transform.position - transform.forward * m_collRadius,
            posMinusY - transform.forward * m_collRadius, out hit))
        {
            groundedRay++;
        }


        if (Physics.Linecast(transform.position + transform.right * m_collRadius,
            posMinusY + transform.right * m_collRadius, out hit))
        {
            groundedRay++;
        }
        if (Physics.Linecast(transform.position - transform.right * m_collRadius,
            posMinusY - transform.right * m_collRadius, out hit))
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

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {

    }

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

            //transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = m_colors[(int)m_currentMode];
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
    #endregion
}