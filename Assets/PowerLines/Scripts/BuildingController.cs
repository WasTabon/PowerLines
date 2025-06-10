using DG.Tweening;
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
            _cell = cell;
            
            if (!cell.HaveBuilding())
            {
                PlaceBuilding(cell);
            }
            else
            {
                ClickOnBuilding(cell);
            }
        }
    }
    
    public void BuildBuilding()
    {
        _cell.SetBuilding(_building.GetComponent<Building>());
        _building = null;
        _cell = null;
        UIController.Instance.HideBuildPanel();
    }
    public void DenyBuild()
    {
        Destroy(_building);
        _building = null;
        _cell = null;
        UIController.Instance.HideBuildPanel();
    }

    private void PlaceBuilding(Cell cell)
    {
        Vector3 spawnPos = new Vector3(cell.transform.position.x, cell.transform.position.y, -1f);
        _building = Instantiate(_currentBuilding.gameObject, spawnPos, _currentBuilding.transform.rotation);
        bool isBuilding = _building.GetComponent<Building>().CheckBuildings();

        if (!isBuilding)
        {
            UIController.Instance.ShowBuildPanel();
        }
        else
        {
            UIController.Instance.PopupCantBuildPanel();
            Destroy(_building);
            _building = null;
            _cell = null;
        }
    }

    private void ClickOnBuilding(Cell cell)
    {
        var building = cell.GetBuilding();
        string name = building.name;
        Transform bTransform = building.gameObject.transform;

        bTransform.localScale = Vector3.one;
        bTransform.DOPunchScale(Vector3.one * 0.35f, 0.4f, 10, 1)
            .OnStart(() =>
            {
                UIController.Instance.SetCurrentBuildingText(name);
            })
            .OnComplete((() =>
            {
                if (bTransform.localScale.x < 1 && bTransform.localScale.y < 1)
                {
                    bTransform.DOScale(Vector3.one, 0.3f);
                }
            }));
    }
}
