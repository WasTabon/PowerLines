using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    private SelectableComponent[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new SelectableComponent[width, height];
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, 0, gridPos.y * cellSize);
    }

    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / cellSize),
            Mathf.RoundToInt(worldPos.z / cellSize)
        );
    }

    public bool IsCellOccupied(Vector2Int pos)
    {
        return GetComponentAt(pos) != null;
    }

    public void SetComponentAt(Vector2Int pos, SelectableComponent component)
    {
        if (IsInBounds(pos))
            grid[pos.x, pos.y] = component;
    }

    public SelectableComponent GetComponentAt(Vector2Int pos)
    {
        if (IsInBounds(pos))
            return grid[pos.x, pos.y];
        return null;
    }

    public bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }
}
