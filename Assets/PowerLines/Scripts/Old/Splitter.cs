using UnityEngine;

public class Splitter : ElectricNode
{
    public override void ReceivePower(float inputVoltage, float inputCurrent)
    {
        voltage = inputVoltage;
        current = inputCurrent;
        Simulate();
    }

    public override void Simulate()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        int targets = 0;
        foreach (var dir in directions)
        {
            var cell = GridManager.Instance.GetCell(gridPosition + dir);
            if (cell?.OccupiedObject is ElectricNode node)
                targets++;
        }

        foreach (var dir in directions)
        {
            var cell = GridManager.Instance.GetCell(gridPosition + dir);
            if (cell?.OccupiedObject is ElectricNode node)
            {
                node.ReceivePower(voltage, current / targets);
            }
        }
    }
}