using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject m_target;
    private LayerMask ignoreLayer;

    [Header("Movement")]
    [SerializeField] private float m_targetDistance = 6f;
    [SerializeField] private float maxTargetDistance = 8f;
    [SerializeField] private float minTargetDistance = 2f;
    [SerializeField] private float m_cameraLerp = 12f;
    private float rotationX;
    private float rotationY;
    private Vector3 offset;

    private void Start()
    {
        if (m_target == null) { m_target = FindObjectOfType<Player>().gameObject.transform.GetChild(1).gameObject; }
        
        ignoreLayer = LayerMask.GetMask("Player");

        //Default values
        if (offset == Vector3.zero) { offset = Vector3.up; }
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0f) { return; }

        //Rotation
        CameraRotation();

        rotationX = Mathf.Clamp(rotationX, 0, 90f);
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

    private void CameraRotation()
    {
        rotationX -= InputManager._INPUT_MANAGER.GetMouseAxis().y / 7;
        rotationY += InputManager._INPUT_MANAGER.GetMouseAxis().x / 7;
    }

    private void Zoom()
    {
        if (InputManager._INPUT_MANAGER.GetWheelAxis() < 0f && m_targetDistance < maxTargetDistance)
        {
            m_targetDistance += 0.5f;
        }
        else if (InputManager._INPUT_MANAGER.GetWheelAxis() > 0f && m_targetDistance > minTargetDistance)
        {
            m_targetDistance -= 0.5f;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_target.transform.position);
    }*/
}