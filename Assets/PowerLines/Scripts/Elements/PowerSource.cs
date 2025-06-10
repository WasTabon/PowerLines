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
    
    protected override bool CanBuild(Building building)
    {
        Debug.Log("True cant build False can built");
        
        return false;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded power");
    }
}