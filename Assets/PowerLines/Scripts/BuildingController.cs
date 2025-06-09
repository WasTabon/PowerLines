using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Building _currentBuilding;
    
    private void Start()
    {
        InputHandler.OnTap += HandleTap;
    }
    
    public void SetCurrentBuilding(Building building)
    {
        _currentBuilding = building;
    }
    
    private void HandleTap(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

        Collider2D col = Physics2D.OverlapPoint(worldPos2D);

        if (col != null && col.gameObject.TryGetComponent(out Cell cell))
        {
            if (!cell.HaveBuilding())
            {
                UIController.Instance.ShowBuildPanel();
            }
        }
    }
}
