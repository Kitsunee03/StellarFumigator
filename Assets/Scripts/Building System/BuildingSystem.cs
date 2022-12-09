using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem m_buildingSystem;
    private Player m_player;

    [Header("Managers")]
    private static InputManager m_input;

    [Header("Grid")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> structures;
    private int currentStructure;
    private int currentStructurePrice;

    private PlaceableObject objectToPlace;

    private void Awake()
    {
        m_buildingSystem = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
    private void Start()
    {
        m_input = InputManager._INPUT_MANAGER;
        m_player = FindObjectOfType<Player>();

        currentStructurePrice = structures[currentStructure].gameObject.GetComponent<Turret>().TurretPrice;
    }
    private void Update()
    {
        //Is player on Constructor mode?
        if (m_player.CurrentMode != PLAYER_MODE.ARCHITECT)
        {
            if (objectToPlace) { Destroy(objectToPlace.gameObject); }
            return;
        }

        //Start building
        if (m_input.GetPrimaryButtonPressed() && !objectToPlace && GameStats.Gems >= currentStructurePrice) 
        { 
            InitializeWithObject(structures[currentStructure]); 
        }

        //Change Structure
        if (m_input.GetPrevBuildingButtonPressed() && !objectToPlace)
        {
            if (currentStructure > 0) { currentStructure--; }
            else { currentStructure = structures.Count - 1; }
            currentStructurePrice = structures[currentStructure].gameObject.GetComponent<Turret>().TurretPrice;
        }
        if (m_input.GetNextBuildingButtonPressed() && !objectToPlace)
        {
            currentStructure = (currentStructure + 1) % structures.Count;
            currentStructurePrice = structures[currentStructure].gameObject.GetComponent<Turret>().TurretPrice;
        }

        //Structure needed to do following code
        if (!objectToPlace) { return; }

        //Rotate Structure
        if (m_input.GetRotateButtonPressed()) { objectToPlace.RotateStructure(); }
        //Place
        else if (m_input.GetPlaceButtonPressed())
        {
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();

                //Set area as occuped
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);

                //Clean object
                objectToPlace = null;
            }
            else { Destroy(objectToPlace.gameObject); }
        }
        //Cancel Placement
        else if (m_input.GetCancelBuildButtonPressed())
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    #region Utils
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

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
    #endregion

    #region Building Placement
    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Utils.GetMouseWorldPosition());
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);

        //Drag Component
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();

        //Try Disable turret
        try { obj.GetComponent<Turret>().enabled = false; }
        catch { }
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
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }
    #endregion

    #region Accessors
    public int GetCurrentStructureNum { get { return currentStructure; } }
    public GameObject GetCurrentStructure { get { return structures[currentStructure]; } }
    #endregion
}