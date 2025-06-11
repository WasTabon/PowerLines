using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Flags]
public enum Direction
{
    None  = 0,
    Up    = 1 << 0,
    Down  = 1 << 1,
    Left  = 1 << 2,
    Right = 1 << 3
}

public abstract class Building : MonoBehaviour
{
    public string name;

    [SerializeField] protected int _volt;

    [SerializeField] protected Vector2 checkBoxSize;
    [SerializeField] protected Direction checkDirections = Direction.Up | Direction.Down;

    private Building _building;
    
    public int Volt => _volt;

    protected bool _isActive;
    
    public void SetActive()
    {
        _isActive = true;
    }

    public void ResetBuilding()
    {
        Debug.Log("Called reset");
        _building = null;
    }
    
    public bool IsOverlapBuilding()
    {
        Dictionary<Direction, Vector2> directionOffsets = new()
        {
            { Direction.Up,    Vector2.up },
            { Direction.Down,  Vector2.down },
            { Direction.Left,  Vector2.left },
            { Direction.Right, Vector2.right }
        };

        _building = null;
        bool hasNeighbor = false;

        foreach (var kvp in directionOffsets)
        {
            if ((checkDirections & kvp.Key) == 0) continue;

            Debug.Log($"Key: {kvp.Key}");

            Vector2 offset = kvp.Value * 0.5f;
            Vector2 checkPosition = (Vector2)transform.position + offset;

            Vector2 boxSize = (kvp.Key == Direction.Up || kvp.Key == Direction.Down)
                ? new Vector2(0.8f, 0.5f)
                : new Vector2(0.5f, 0.8f);

            Collider2D[] hits = Physics2D.OverlapBoxAll(checkPosition, boxSize, 0f);

            var buildings = hits
                .Select(hit => hit.GetComponent<Building>())
                .Where(building => building != null && building != this)
                .ToArray();

            foreach (Building buildingHit in buildings)
            {
                Debug.Log($"Building: {buildingHit.gameObject.name}", buildingHit.gameObject);
            }

            if (buildings.Length > 0)
            {
                _building = buildings[0];
                hasNeighbor = true;
            }
        }

        if (!hasNeighbor)
        {
            Debug.Log("No buildings found in any direction");
            return CanBuild(null);
        }

        Debug.Log($"Give building to CanBuild: {_building?.gameObject.name}", _building?.gameObject);
        return CanBuild(_building);
    }

    protected virtual bool CanBuild(Building building)
    {
        Debug.Log("Building");
        
        if (building != null)
            return true;

        return false;
    }

    public virtual void OnBuild()
    {
        Debug.Log("Placed");
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Dictionary<Direction, Vector2> directionOffsets = new()
        {
            { Direction.Up,    (Vector2)transform.up * checkBoxSize.y },
            { Direction.Down, -(Vector2)transform.up * checkBoxSize.y },
            { Direction.Left, -(Vector2)transform.right * checkBoxSize.x },
            { Direction.Right, (Vector2)transform.right * checkBoxSize.x }
        };

        foreach (var kvp in directionOffsets)
        {
            if ((checkDirections & kvp.Key) == 0) continue;

            Vector2 checkPosition = (Vector2)transform.position + kvp.Value;
            Vector2 boxSize = (kvp.Key == Direction.Up || kvp.Key == Direction.Down)
                ? new Vector2(0.8f, 0.5f)
                : new Vector2(0.5f, 0.8f);

            Gizmos.DrawWireCube(checkPosition, boxSize);
        }
    }
}
