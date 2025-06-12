using UnityEngine;

public class Cell : MonoBehaviour
{
    public Building _building;
    
    public void SetBuilding(Building building) => _building = building;

    public Building GetBuilding() => _building;
    public bool HaveBuilding() => _building != null;

    public void ResetBuilding() => _building = null;
}