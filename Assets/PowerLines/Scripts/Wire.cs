using UnityEngine;

public class Wire : ElectricNode
{
    public float resistancePerTile = 1f;
    public ElectricNode input;

    public override void ReceivePower(float inputVoltage, float inputCurrent)
    {
        float drop = resistancePerTile;
        voltage = Mathf.Max(0, inputVoltage - drop);
        current = inputCurrent;
        Simulate();
    }

    public override void Simulate()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var dir in directions)
        {
            Vector2Int nextPos = gridPosition + dir;
            GridCell nextCell = GridManager.Instance.GetCell(nextPos);
            if (nextCell?.OccupiedObject is ElectricNode node && node != input)
            {
                node.ReceivePower(voltage, current);
            }
        }
    }
}