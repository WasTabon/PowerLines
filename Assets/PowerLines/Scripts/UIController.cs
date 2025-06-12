using DG.Tweening;
using Febucci.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [SerializeField] private RectTransform _buildPanel;
    [SerializeField] private RectTransform _cantBuildPanel;
    [SerializeField] private RectTransform _voltPanel;
    
    [SerializeField] TypewriterByCharacter _currentBuildingText;
    [SerializeField] TypewriterByCharacter _voltText;
    [SerializeField] TypewriterByCharacter _currentBuildingVoltText;

    private int _voltage;
    private Transformer _transformer;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildPanel.DOAnchorPosY(-240f, 0);
        _cantBuildPanel.DOAnchorPosY(-240f, 0);
    }

    public void SetCurrentBuildingText(string name, string volt)
    {
        _currentBuildingText.ShowText($"Current Building:\n{name}");
        _currentBuildingVoltText.ShowText($"Volt: {volt}");
    }
    
    public void ShowBuildPanel()
    {
        _buildPanel.DOKill();
        _buildPanel.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.InOutBack);
    }

    public void HideBuildPanel()
    {
        _buildPanel.DOKill();
        _buildPanel.DOAnchorPosY(-240f, 0.5f)
            .SetEase(Ease.InOutBack);
    }

    public void ShowVoltPanel(Transformer transformer)
    {
        _transformer = transformer;
        _voltage = 5;
        _voltPanel.gameObject.SetActive(true);
        _voltPanel.DOScale(Vector3.zero, 0f)
            .OnComplete((() =>
            {
                _voltPanel.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.InOutBack)
                    .OnComplete(() =>
                    {
                        _voltText.ShowText($"Current volt: {transformer.Volt}");
                    });
            }));
    }

    public void SetVoltage()
    {
        _transformer.SetVolt(_voltage);
        _voltPanel.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InOutBack)
            .OnComplete(() =>
            {
                _voltPanel.gameObject.SetActive(false);
            });
    }

    public void AddVolt()
    {
        if (_voltage < 10)
        {
            _voltage++;
        }
    }

    public void TakeVolt()
    {
        if (_voltage > 1)
        {
            _voltage--;
        }
    }
    
    public void PopupCantBuildPanel()
    {
        _cantBuildPanel.DOKill();
        
        if (_cantBuildPanel.anchoredPosition.y > -10f)
        {
            _cantBuildPanel.localScale = Vector3.one;
            _cantBuildPanel.DOPunchScale(Vector3.one * 0.25f, 0.3f, 10, 1)
                .OnComplete((() =>
                {
                    if (_cantBuildPanel.localScale.x < 1 && _cantBuildPanel.localScale.y < 1)
                    {
                        _cantBuildPanel.DOScale(Vector3.one, 0.3f);
                    }
                }));
        }
        
        _cantBuildPanel.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.InOutBack)
            .OnComplete((() =>
            {
                _cantBuildPanel.DOAnchorPosY(-240f, 0.5f)
                    .SetDelay(0.5f)
                    .SetEase(Ease.InOutBack);
            }));
    }
}
