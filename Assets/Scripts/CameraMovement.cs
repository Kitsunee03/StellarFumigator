using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Player")]
    private GameObject m_target;
    private LayerMask ignoreLayer;

    [Header("Movement")]
    private float m_targetDistance;
    private float m_cameraLerp;
    private float rotationX, rotationY;
    private Vector3 offset;

    private void Start()
    {
        m_target = FindObjectOfType<Player>().gameObject.transform.GetChild(1).gameObject;
        ignoreLayer = LayerMask.GetMask("Player");

        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        //Default values
        if (m_targetDistance == 0f) { m_targetDistance = 8f; }
        if (m_cameraLerp == 0f) { m_cameraLerp = 12f; }
        if (offset == Vector3.zero) { offset = Vector3.up; }
    }

    private void LateUpdate()
    {
        //Rotation
        CamRotation();

        rotationX = Mathf.Clamp(rotationX, 0, 80f);
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        //Movement
        Zoom();

        Vector3 finalPosition = Vector3.Lerp(transform.position, m_target.transform.position - transform.forward * m_targetDistance /*+ offset*/, m_cameraLerp * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Linecast(m_target.transform.position, finalPosition, out hit, ~ignoreLayer))
        {
            finalPosition = hit.point;
        }

        transform.position = finalPosition;
    }

    private void CamRotation()
    {
        if (Gamepad.all.Count == 0)
        {
            rotationX -= InputManager._INPUT_MANAGER.GetMouseAxis().y / 7;
            rotationY += InputManager._INPUT_MANAGER.GetMouseAxis().x / 7;
        }
        else
        {
            rotationX -= InputManager._INPUT_MANAGER.GetMouseAxis().y;
            rotationY += InputManager._INPUT_MANAGER.GetMouseAxis().x;
        }
    }
    private void Zoom()
    {
        if (InputManager._INPUT_MANAGER.GetWheelAxis() < 0f && m_targetDistance < 8f)
        {
            m_targetDistance += 0.5f;
        }
        else if (InputManager._INPUT_MANAGER.GetWheelAxis() > 0f && m_targetDistance > 2f)
        {
            m_targetDistance -= 0.5f;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_target.transform.position);
    }*/

    //public Vector2 camRotation { get { return new Vector2(rotationX, rotationY); } }
}