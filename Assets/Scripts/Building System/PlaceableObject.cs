using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private Vector3[] Vertices;
    [SerializeField] private GameObject buildVFX;

    private void Start()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    public void Place()
    {
        Destroy(gameObject.GetComponent<ObjectDrag>());

        Placed = true;

        //<Events Of Placement>

        //Pay construction cost
        GameStats.Gems -= gameObject.GetComponent<Turret>().TurretPrice;
        //Try enable turret
        try { gameObject.GetComponent<Turret>().enabled = true; }
        catch { }
        //Build VFX
        GameObject effectIns = Instantiate(buildVFX, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
    }

    public void RotateStructure()
    {
        transform.Rotate(0, 90, 0);

        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] vertices = new Vector3[Vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }
        Vertices = vertices;
    }

    private void GetColliderVertexPositionsLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];

        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }
    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.m_buildingSystem.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x),
                                Mathf.Abs((vertices[0] - vertices[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
}