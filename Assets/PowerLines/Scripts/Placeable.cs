using UnityEngine;

public abstract class Placeable : MonoBehaviour
{
    public Vector2Int gridPosition;
    public abstract void Simulate();
}
