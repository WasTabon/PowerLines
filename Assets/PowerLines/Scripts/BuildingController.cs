using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Building _currentBuilding;

    private GameObject _building;
    private Cell _cell;
    
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
        if (_currentBuilding == null)
            return;
        if (_building != null)
            return;
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

        Collider2D col = Physics2D.OverlapPoint(worldPos2D);

        if (col != null && col.gameObject.TryGetComponent(out Cell cell))
        {
            if (!cell.HaveBuilding())
            {
                Vector3 spawnPos = new Vector3(cell.gameObject.transform.localPosition.x,
                    cell.gameObject.transform.localPosition.y, -1f);
                _building = Instantiate(_currentBuilding.gameObject, spawnPos,
                    _currentBuilding.transform.rotation);
                _cell = cell;
                UIController.Instance.ShowBuildPanel();
            }
        }
    }

    public void DenyBuild()
    {
        Destroy(_building);
        _building = null;
        _cell = null;
        UIController.Instance.HideBuildPanel();
    }

    public void BuildBuilding()
    {
        _cell.SetBuilding(_building.GetComponent<Building>());
        _building = null;
        _cell = null;
        UIController.Instance.HideBuildPanel();
    }
}
