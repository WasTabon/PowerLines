using UnityEngine;

public class PowerSource : Building
{
    private void Awake()
    {
        checkDirections = Direction.Up | Direction.Left;
    }

    public void SetVolt(int amount)
    {
        _volt = amount;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded power");
    }
}