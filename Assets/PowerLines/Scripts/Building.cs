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

    public bool IsOverlapBuilding()
    {
        _building = null;
        
        Dictionary<Direction, Vector2> directionOffsets = new()
        {
            { Direction.Up,    Vector2.up },
            { Direction.Down,  Vector2.down },
            { Direction.Left,  Vector2.left },
            { Direction.Right, Vector2.right }
        };

        foreach (var kvp in directionOffsets)
        {
            if ((checkDirections & kvp.Key) == 0) continue;

            Vector2 offset = kvp.Value * 0.5f;
            Vector2 checkPosition = (Vector2)transform.position + offset;

            Vector2 boxSize = (kvp.Key == Direction.Up || kvp.Key == Direction.Down)
                ? new Vector2(0.8f, 0.5f)
                : new Vector2(0.5f, 0.8f);

            Collider2D[] hits = Physics2D.OverlapBoxAll(checkPosition, boxSize, 0f);
            
            var buildings = hits
                .Select(hit => hit.GetComponent<Building>())
                .Where(building => building != null)
                .ToArray();

            foreach (Building building2 in buildings)
            {
                Debug.Log($"All overlap on {gameObject.name} - {building2.gameObject.name}", building2.gameObject);
            }
            
            if (buildings.Length <= 1)
            {
                return CanBuild(null);
            }
            
            foreach (var hit in hits)
            {
                Building building = hit.GetComponent<Building>();
                if (building != null && building != this)
                {
                    _building = building;
                }
            }
        }

        if (_building == null)
        {
            Debug.Log("Building is null");
            return true;
        }

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
