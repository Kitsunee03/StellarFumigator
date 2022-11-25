using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem m_buildingSystem;
    
    [Header("Managers")]
    private static InputManager m_input;

    [Header("Grid")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    [Header("Prefabs")]
    [SerializeField] private GameObject structure1;
    [SerializeField] private GameObject structure2;

    private PlaceableObject objectToPlace;
    
    #region Unity Methods
    private void Awake()
    {
        m_buildingSystem = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
    private void Start()
    {
        m_input = InputManager._INPUT_MANAGER;
    }
    private void Update()
    {
        //Inicia construcción
        if (m_input.GetPrimaryButtonPressed() && !objectToPlace) { InitializeWithObject(structure1);}

        if (!objectToPlace) { return; }

        if (m_input.GetJumpButtonPressed())
        {
            //Place
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                //Set area as occuped
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);

                objectToPlace = null;
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
        //Cancelar Placement
        /*else if (m_input.botondecancelar)
        {
            Destroy(objectToPlace.gameObject);
        }*/
    }
    #endregion

    #region Utils
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else { return Vector3.zero; }
    }
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }
    #endregion

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];

        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    #region Building Placement
    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach (var b in baseArray)
        {
            if(b == whiteTile)
            {
                return false;
            }
        }
        Debug.Log("miausi");
        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, 
                            start.x + size.x, start.y + size.y);
    }
    #endregion
}