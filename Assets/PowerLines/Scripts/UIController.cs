using DG.Tweening;
using Febucci.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    
    [SerializeField] private RectTransform _buildPanel;
    
    [SerializeField] TypewriterByCharacter _currentBuildingText;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildPanel.DOAnchorPosY(-240f, 0);
    }

    public void SetCurrentBuildingText(string name)
    {
        _currentBuildingText.ShowText($"Current Building:\n{name}");
    }
    
    public void ShowBuildPanel()
    {
        Debug.Log("Showed");
        _buildPanel.DOKill();
        _buildPanel.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.InOutBack);
    }

    public void HideBuildPanel()
    {
        Debug.Log("Clicked");
        _buildPanel.DOKill();
        _buildPanel.DOAnchorPosY(-240f, 0.5f)
            .SetEase(Ease.InOutBack);
    }
}
