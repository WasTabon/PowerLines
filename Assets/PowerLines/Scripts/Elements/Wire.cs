using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Wire : Building
{
    [SerializedDictionary("Prefab", "Sides")]
    public SerializedDictionary<Building, Direction> allowedPrefabs;

    protected override bool CanBuild(Building building, Direction direction)
    {
        if (building == null)
        {
            Debug.Log("No neighbor – allow build?");
            return true;
        }

        foreach (var kvp in allowedPrefabs)
        {
            Debug.Log($"In cycle: {kvp.Key.GetType().Name}, allowed dirs: {kvp.Value}");
            Debug.Log($"Detected direction: {direction}");

            if (building.GetType() == kvp.Key.GetType())
            {
                Debug.Log("Type is good");
                Direction allowedDir = kvp.Value;
                
                if ((allowedDir & direction) != 0)
                {
                    Debug.Log("Direction is valid");
                    _volt = building.Volt - 1;
                    return false;
                }
                else
                {
                    Debug.Log($"Invalid direction: {direction} not in {allowedDir}");
                    return true;
                }
            }
        }

        return true;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded wire");
    }
}

