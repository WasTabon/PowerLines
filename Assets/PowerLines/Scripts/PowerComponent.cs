using UnityEngine;
using System.Collections.Generic;

public class PowerComponent : SelectableComponent
{
    public float voltage;
    public float current;
    public float resistance;

    public bool isSource;
    public bool isConsumer;
    public float requiredVoltage;
    public float requiredCurrent;

    public bool isPowered;

    public void CalculatePowerFlow()
    {
        if (isSource)
        {
            SpreadPower(voltage, current, new List<Vector2Int>());
        }
    }

    private void SpreadPower(float v, float c, List<Vector2Int> visited)
    {
        isPowered = true;
        visited.Add(gridPosition);

        foreach (var offset in new Vector2Int[] {
                     Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
                 })
        {
            Vector2Int nextPos = gridPosition + offset;
            if (!GridManager.Instance.IsInBounds(nextPos) || visited.Contains(nextPos))
                continue;

            var next = GridManager.Instance.GetComponentAt(nextPos) as PowerComponent;
            if (next != null && !next.isPowered)
            {
                float newVoltage = Mathf.Max(0, v - resistance);
                float newCurrent = c;

                if (next.isConsumer)
                {
                    next.isPowered = (newVoltage >= next.requiredVoltage && newCurrent >= next.requiredCurrent);
                }
                else
                {
                    next.SpreadPower(newVoltage, newCurrent, new List<Vector2Int>(visited));
                }
            }
        }
    }
}