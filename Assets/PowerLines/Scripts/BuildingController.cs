using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Building _currentBuilding;

    [SerializeField] private LayerMask cellLayerMask; 
    
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
        if (_currentBuilding == null || _building != null)
            return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

        RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero, 0f, cellLayerMask);

        if (hit.collider != null && hit.collider.TryGetComponent(out Cell cell))
        {
            if (!cell.HaveBuilding())
            {
                Vector3 spawnPos = new Vector3(cell.transform.position.x, cell.transform.position.y, -1f);
                _building = Instantiate(_currentBuilding.gameObject, spawnPos, _currentBuilding.transform.rotation);
                _cell = cell;
                UIController.Instance.ShowBuildPanel();
            }
            else
            {
                string name = cell.GetBuilding().name;
                UIController.Instance.SetCurrentBuildingText(name);
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
        _building.GetComponent<Building>().CheckBuildings();
        _building = null;
        _cell = null;
        UIController.Instance.HideBuildPanel();
    }
}
