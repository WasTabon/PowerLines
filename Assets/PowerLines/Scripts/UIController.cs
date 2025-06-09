using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private RectTransform _buildPanel;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildPanel.DOAnchorPosY(-240f, 0);
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
