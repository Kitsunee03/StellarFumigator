using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;

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
        Animating(m_player.GetMovementAxis().x, m_player.GetMovementAxis().z);
        //m_anim.SetFloat("MovementX", dirX, dampingTime, Time.deltaTime);
        //m_anim.SetFloat("MovementY", dirY, dampingTime, Time.deltaTime);
        //Attack
        m_anim.SetBool("isAttacking", m_player.IsAttacking);
    }

    private Vector3 moveDirection = Vector3.zero;

    void Animating(float h, float v)
    {
        moveDirection = new Vector3(h, 0, v);

        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection = moveDirection.normalized;
        }

        moveDirection = transform.InverseTransformDirection(moveDirection);

        m_anim.SetFloat("MovementX", moveDirection.x, dampingTime, Time.deltaTime);
        m_anim.SetFloat("MovementY", moveDirection.z, dampingTime, Time.deltaTime);
    }
}