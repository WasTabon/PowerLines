using DG.Tweening;
using Febucci.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [SerializeField] private RectTransform _buildPanel;
    [SerializeField] private RectTransform _cantBuildPanel;
    
    [SerializeField] TypewriterByCharacter _currentBuildingText;
    [SerializeField] TypewriterByCharacter _currentBuildingVoltText;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildPanel.DOAnchorPosY(-240f, 0);
        _cantBuildPanel.DOAnchorPosY(-240f, 0);
    }

    public void SetCurrentBuildingText(string name)
    {
        _currentBuildingText.ShowText($"Current Building:\n{name}");
        _currentBuildingText.ShowText($"Current Building:\n{name}");
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
    
    public void PopupCantBuildPanel()
    {
        _cantBuildPanel.DOKill();
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
