using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour
{
    private GameObject player;
    private float maxBuildDistance = 8;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 pos = Utils.GetMouseWorldPosition();
        if (Vector3.Distance(player.transform.position, pos) < maxBuildDistance)
        {
            transform.position = BuildingSystem.m_buildingSystem.SnapCoordinateToGrid(pos);
        }
    }
}