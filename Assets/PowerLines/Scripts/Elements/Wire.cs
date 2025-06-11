using System.Collections.Generic;
using UnityEngine;

public class Wire : Building
{
    [Header("Разрешённые типы построек (префабы)")]
    [SerializeField] private List<Building> allowedPrefabs;

    protected override bool CanBuild(Building building)
    {
        if (building == null)
        {
            Debug.Log("Building is null in wire");
            return true;
        }

        foreach (var prefab in allowedPrefabs)
        {
            if (prefab != null && building.GetType() == prefab.GetType())
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

