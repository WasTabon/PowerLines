using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Wire : Building
{
    [SerializedDictionary("Prefab", "Sides")]
    public SerializedDictionary<Building, Direction> allowedPrefabs;

    protected override bool CanBuild(Building building)
    {
        if (building == null)
        {
            Debug.Log("Building is null in wire");
            return true;
        }

        foreach (var prefab in allowedPrefabs.Keys)
        {
            if (building.GetType() == prefab.GetType())
            {
                _volt = building.Volt - 1;
                return false;
            }
        }

        return true;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded wire");
    }
}

