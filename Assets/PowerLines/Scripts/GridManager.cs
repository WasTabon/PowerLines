using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width, height;
    public float cellSize = 1f;
    private GridCell[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            grid[x, y] = new GridCell(new Vector2Int(x, y));
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0);
    }

    public bool IsInBounds(Vector2Int pos) =>
        pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;

    public GridCell GetCell(Vector2Int pos)
    {
        if (!IsInBounds(pos)) return null;
        return grid[pos.x, pos.y];
    }
}

public class GridCell
{
    public Vector2Int Position;
    public Placeable OccupiedObject;

    public GridCell(Vector2Int pos)
    {
        Position = pos;
    }
}
