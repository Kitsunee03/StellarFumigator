using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator m_anim;
    private Player m_player;

    private float dampingTime = 0.3f;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_player = GetComponent<Player>();
    }

    private void Update()
    {
        //Idle
        m_anim.SetBool("isMoving", m_player.IsMoving());
        //Move
        SetAnimMovement(m_player.GetMovementDir().x, m_player.GetMovementDir().z);
        //Attack
        m_anim.SetBool("isAttacking", m_player.IsAttacking);
        //Dash
        m_anim.SetBool("isDashing", m_player.IsDashing);
    }

    void SetAnimMovement(float h, float v)
    {
        Vector3 moveDirection = new Vector3(h, 0, v);
        if (moveDirection.magnitude > 1.0f) { moveDirection = moveDirection.normalized; }
        moveDirection = transform.InverseTransformDirection(moveDirection);

        m_anim.SetFloat("MovementX", moveDirection.x, dampingTime, Time.deltaTime);
        m_anim.SetFloat("MovementY", moveDirection.z, dampingTime, Time.deltaTime);
    }
}