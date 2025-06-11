using UnityEngine;

public class Wire : Building
{
    protected override bool CanBuild(Building building)
    {
        if (building == null)
        {
            Debug.Log("Building is null");
            return true;
        }
        if (building is PowerSource || building is Wire)
        {
            Debug.Log("Building is no null", building.gameObject);
            _volt = building.Volt - 1;
            return false;
        }

        Debug.Log("None");
        
        return true;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded wire");
    }
    
    //треба пофіксити то шо ставиться wire коли вокруг нема нічого
}
