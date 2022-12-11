using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils m_utils;

    private void Awake()
    {
        m_utils = this;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit,999f,~LayerMask.GetMask("Player")))
        {
            return raycastHit.point;
        }
        else { return Vector3.zero; }
    }

    public static GameObject GetMousePointingObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, ~LayerMask.GetMask("Player")))
        {
            return raycastHit.transform.gameObject;
        }
        else { return new GameObject(); }
    }
}