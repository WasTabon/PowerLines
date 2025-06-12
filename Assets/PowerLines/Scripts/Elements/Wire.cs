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

        bool matchedType = false;

        foreach (var kvp in allowedPrefabs)
        {
            if (building.gameObject.name == kvp.Key.gameObject.name)
            {
                matchedType = true;
                Debug.Log($"Matched type: {kvp.Key.GetType().Name}, allowed dirs: {kvp.Value}");
                Debug.Log($"Detected direction: {direction}");

                if ((kvp.Value & direction) != 0)
                {
                    Debug.Log("Direction is valid");
                    _volt = building.Volt - 1;
                    matchedType = true;
                }
            }
            else
            {
                Debug.Log("Building is missing in dictionary");
            }
        }

        if (matchedType)
        {
            Debug.Log("Type matched");
            return false;
        }

        return true;
    }

    public override void OnBuild()
    {
        Debug.Log("Builded wire");
    }
}

