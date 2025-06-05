using UnityEngine;

public class PowerSource : ElectricNode
{
    public float outputVoltage = 10f;
    public float maxCurrent = 10f;

    void Start()
    {
        Simulate();
    }

    public override void Simulate()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var dir in directions)
        {
            GridCell neighbor = GridManager.Instance.GetCell(gridPosition + dir);
            if (neighbor?.OccupiedObject is ElectricNode node)
            {
                node.ReceivePower(outputVoltage, maxCurrent / directions.Length);
            }
        }
    }
}