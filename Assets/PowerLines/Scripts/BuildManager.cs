using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject selectedPrefab;
    public Camera mainCamera;

    void Awake() => Instance = this;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // touch or mouse
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = GridManager.Instance.WorldToGrid(worldPos);
            GridCell cell = GridManager.Instance.GetCell(gridPos);

            if (cell != null && cell.OccupiedObject == null && selectedPrefab != null)
            {
                GameObject obj = Instantiate(selectedPrefab, GridManager.Instance.GridToWorld(gridPos), Quaternion.identity);
                Placeable placeable = obj.GetComponent<Placeable>();
                placeable.gridPosition = gridPos;
                cell.OccupiedObject = placeable;
            }
        }
    }

    public void SetPrefab(GameObject prefab)
    {
        selectedPrefab = prefab;
    }
}