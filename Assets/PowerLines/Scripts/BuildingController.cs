using DG.Tweening;
    using UnityEngine;

    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private int _startVolt;
        [SerializeField] private Cell _cellStartGame;
        [SerializeField] private Cell _cellFactory;
        [SerializeField] private PowerSource _powerSourceStartGame;
        [SerializeField] private Consumer _consumer;
        
        private Building _currentBuilding;
        private Consumer _buildedConsumer;

        [SerializeField] private LayerMask cellLayerMask; 
        
        private GameObject _building;
        private Cell _cell;

        private void Awake()
        {
            PowerSource powerSource = Instantiate(_powerSourceStartGame.gameObject, _cellStartGame.gameObject.transform.localPosition, _powerSourceStartGame.gameObject.transform.localRotation).GetComponent<PowerSource>();
            powerSource.gameObject.name = "PowerSource";
            powerSource.SetVolt(_startVolt);
            _cellStartGame._building = powerSource;
            
            Consumer consumer = Instantiate(_consumer.gameObject, _cellFactory.gameObject.transform.localPosition, _consumer.gameObject.transform.localRotation).GetComponent<Consumer>();
            consumer.gameObject.name = "Consumer";
            _cellFactory._building = consumer;
            _buildedConsumer = consumer;
        }

        private void Start()
        {
            InputHandler.OnTap += HandleTap;
            ClickOnBuilding(_cellStartGame);
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
                    if (cell._building is not Consumer)
                        ClickOnBuilding(cell);
                }
            }
        }
        
        public void BuildBuilding()
        {
            _building.GetComponent<Building>().OnBuild();
            
            _cell.SetBuilding(_building.GetComponent<Building>());
            
            ClickOnBuilding(_cell);
            
            _building = null;
            _cell = null;
            UIController.Instance.HideBuildPanel();
            _consumer.GetComponent<BoxCollider2D>().enabled = true;
        }
        public void DenyBuild()
        {
            Destroy(_building);
            _building = null;
            _cell = null;
            UIController.Instance.HideBuildPanel();
            _consumer.GetComponent<BoxCollider2D>().enabled = true;
        }

        private void PlaceBuilding(Cell cell)
        {
            Vector3 spawnPos = new Vector3(cell.transform.position.x, cell.transform.position.y, -1f);
            _building = Instantiate(_currentBuilding.gameObject, spawnPos, _currentBuilding.transform.rotation);
            _building.gameObject.name = _currentBuilding.gameObject.name;
            _building.GetComponent<Building>().ResetBuilding();
            bool isOverlapBuilding = _building.GetComponent<Building>().IsOverlapBuilding();
            
            if (!isOverlapBuilding && _building.GetComponent<Building>().Volt > 0)
            {
                UIController.Instance.ShowBuildPanel();
            }
            else if (_building.GetComponent<Building>().Volt <= 0 && !isOverlapBuilding)
            {
                UIController.Instance.PopupVoltTooLowPanel();
                Destroy(_building);
                _building = null;
                _cell = null;
                _consumer.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                UIController.Instance.PopupCantBuildPanel();
                Destroy(_building);
                _building = null;
                _cell = null;
                _consumer.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        private void ClickOnBuilding(Cell cell)
        {
            var building = cell.GetBuilding();
            string name = building.name;
            string volt = building.Volt.ToString();
            Transform bTransform = building.gameObject.transform;

            bTransform.localScale = Vector3.one;
            bTransform.DOPunchScale(Vector3.one * 0.35f, 0.4f, 10, 1)
                .OnStart(() =>
                {
                    UIController.Instance.SetCurrentBuildingText(name, volt);
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
