using System.Collections.Generic;
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
    [SerializeField] private Tilemap BuildTilemap;
    [SerializeField] private TileBase whiteTile;

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> structures;
    private int currentStructure;
    private int currentStructurePrice;

    private PlaceableObject objectToPlace;
    private Vector3 lastObjectPos;

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
        Color color;
        //Is player on Constructor mode?
        if (m_player.CurrentMode != PLAYER_MODE.ARCHITECT)
        {
            if (objectToPlace) { Destroy(objectToPlace.gameObject); CleanPlaceArea(); }
            color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
            MainTilemap.color = color;
            return;
        }
        color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        MainTilemap.color = color;


        //Start building
        if (m_input.GetPrimaryButtonPressed() && !objectToPlace && GameStats.Gems >= currentStructurePrice)
        {
            InitializeWithObject(structures[currentStructure]);

            Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
            PaintPlaceArea(start, objectToPlace.Size, whiteTile);
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

                //Clean object and BuildGrid
                CleanPlaceArea();
                objectToPlace = null;
                return;
            }
        }
        //Cancel Placement
        else if (m_input.GetCancelBuildButtonPressed()) { Destroy(objectToPlace.gameObject); CleanPlaceArea(); }

        //BuildTilemap color
        if (CanBePlaced(objectToPlace) && objectToPlace != null) { BuildTilemap.color = Color.green; }
        else if (objectToPlace != null) { BuildTilemap.color = Color.red; }
        //BuildTilemap paint and clear
        if (objectToPlace != null && lastObjectPos != objectToPlace.CurrentPosition)
        {
            lastObjectPos = objectToPlace.CurrentPosition;
            Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
            CleanPlaceArea();
            PaintPlaceArea(start, objectToPlace.Size, whiteTile);
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
        foreach (var b in baseArray) { if (b == whiteTile) { return false; } }

        return true;
    }

    private void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }

    private void PaintPlaceArea(Vector3Int start, Vector3Int size, TileBase tile)
    {
        BuildTilemap.BoxFill(start, tile, start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }
    private void CleanPlaceArea()
    {
        BuildTilemap.ClearAllTiles();
    }
    #endregion

    #region Accessors
    public int GetCurrentStructureNum { get { return currentStructure; } }
    public GameObject GetCurrentStructure { get { return structures[currentStructure]; } }
    public PlaceableObject GetStructureToPlace { get { return objectToPlace; } }
    #endregion
}