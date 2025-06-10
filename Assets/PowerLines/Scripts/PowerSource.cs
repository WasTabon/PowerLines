using UnityEngine;

public class PowerSource : Building
{
    private void Awake()
    {
        checkDirections = Direction.Up | Direction.Left;
    }

    protected override bool CanBuild(Building building)
    {
        Debug.Log("True cant build False can built");

        return true;
    }
}