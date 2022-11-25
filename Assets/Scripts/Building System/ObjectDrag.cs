using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void Update()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.m_buildingSystem.SnapCoordinateToGrid(pos);
    }
}