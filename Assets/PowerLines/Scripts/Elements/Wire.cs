using UnityEngine;

public class Wire : Building
{
    protected override bool CanBuild(Building building)
    {
        if (building == null)
        {
            return true;
        }
        if (building is PowerSource || building is Wire)
        {
            _volt = building.Volt - 1;
            return false;
        }

        return true;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded power");
    }
}
