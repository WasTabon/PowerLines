using UnityEngine;

public class Cell : MonoBehaviour
{
    private Building _building;
    
    public void SetBuilding(Building building) => _building = building;

    public Building GetBuilding => _building;

    public void ResetBuilding() => _building = null;
}