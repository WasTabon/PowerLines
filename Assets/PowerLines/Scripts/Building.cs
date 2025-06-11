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
                .Where(building => building != null && building != this)
                .ToArray();

            foreach (var buildingHit in buildings)
            {
                Direction dirToNeighbor = GetDirectionToNeighbor(buildingHit.transform.position);
                if (!CanBuild(buildingHit, dirToNeighbor)) return false;
            }
        }

        return CanBuild(null, Direction.None);
    }

    protected Direction GetDirectionToNeighbor(Vector2 neighborPosition)
    {
        Vector2 delta = neighborPosition - (Vector2)transform.position;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? Direction.Right : Direction.Left;
        }
        else
        {
            return delta.y > 0 ? Direction.Up : Direction.Down;
        }
    }

    
    protected virtual bool CanBuild(Building building, Direction direction)
    {
        Debug.Log("True cant build False can built");

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
