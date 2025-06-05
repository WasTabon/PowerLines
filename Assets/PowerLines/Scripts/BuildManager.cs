using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject[] placeablePrefabs;
    private int selectedIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SetSelectedIndex(int index)
    {
        selectedIndex = index;
    }

    public void TryBuildAt(Vector3 worldPosition)
    {
        Vector2Int gridPos = GridManager.Instance.GetGridPosition(worldPosition);
        if (GridManager.Instance.IsCellOccupied(gridPos)) return;

        GameObject prefab = placeablePrefabs[selectedIndex];
        GameObject obj = Instantiate(prefab, GridManager.Instance.GetWorldPosition(gridPos), Quaternion.identity);
        var component = obj.GetComponent<SelectableComponent>();
        component.gridPosition = gridPos;

        GridManager.Instance.SetComponentAt(gridPos, component);
    }
}