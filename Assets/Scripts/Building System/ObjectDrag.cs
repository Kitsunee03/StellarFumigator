using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - Utils.GetMouseWorldPosition();
    }

    private void Update()
    {
        Vector3 pos = Utils.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.m_buildingSystem.SnapCoordinateToGrid(pos);
    }
}