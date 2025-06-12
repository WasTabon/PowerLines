using DG.Tweening;
using Febucci.UI;
using PowerLines.Scripts;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private RectTransform _winPanel;
    [SerializeField] private RectTransform _star1;
    [SerializeField] private RectTransform _star2;
    [SerializeField] private RectTransform _star3;
    [SerializeField] private RectTransform _continueButton;
    [SerializeField] private RectTransform _winText;
    
    [SerializeField] private AudioClip _winPanelSound;
    [SerializeField] private AudioClip _starSound;
    [SerializeField] private AudioClip _continueButtonSound;
    
    [SerializeField] private RectTransform _buildPanel;
    [SerializeField] private RectTransform _cantBuildPanel;
    [SerializeField] private RectTransform _voltPanel;
    [SerializeField] private RectTransform _voltTooLowPanel;
    
    [SerializeField] TypewriterByCharacter _currentBuildingText;
    [SerializeField] TypewriterByCharacter _voltText;
    [SerializeField] TextMeshProUGUI _voltTextTMP;
    [SerializeField] TextMeshProUGUI _currentVoltText;
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
        _voltTooLowPanel.DOAnchorPosY(-240f, 0);
        
        _winPanel.localScale = Vector3.zero;
        _star1.localScale = Vector3.zero;
        _star2.localScale = Vector3.zero;
        _star3.localScale = Vector3.zero;
        _continueButton.localScale = Vector3.zero;
        _continueButton.gameObject.SetActive(false);
    }

    public void SetCurrentBuildingText(string name, string volt)
    {
        _currentBuildingText.ShowText($"Current Building:\n{name}");
        _currentBuildingVoltText.ShowText($"Volt: {volt}");
    }

    public void ShowWinPanel()
{
    _winPanel.gameObject.SetActive(true);
    
    _winText.anchoredPosition = new Vector2(-1000f, _winText.anchoredPosition.y);
    _continueButton.anchoredPosition = new Vector2(1000f, _continueButton.anchoredPosition.y);
    _star1.localScale = Vector3.zero;
    _star2.localScale = Vector3.zero;
    _star3.localScale = Vector3.zero;
    _continueButton.localScale = Vector3.one;
    _continueButton.gameObject.SetActive(false);

    _winPanel.DOScale(Vector3.one, 0.5f)
        .SetEase(Ease.OutBack)
        .OnStart(() =>
        {
            TryPlaySound(_winPanelSound);
        })
        .OnComplete(() =>
        {
            _winText.DOAnchorPosX(0f, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _winText.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1);

                    _star1.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack)
                        .OnStart(() => TryPlaySound(_starSound))
                        .OnComplete(() =>
                        {
                            _star2.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack)
                                .OnStart(() => TryPlaySound(_starSound))
                                .OnComplete(() =>
                                {
                                    _star3.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack)
                                        .OnStart(() => TryPlaySound(_starSound))
                                        .OnComplete(() =>
                                        {
                                            _continueButton.gameObject.SetActive(true);
                                            _continueButton.DOAnchorPosX(0f, 0.5f)
                                                .SetEase(Ease.OutBack)
                                                .OnStart(() => TryPlaySound(_continueButtonSound))
                                                .OnComplete(() =>
                                                {
                                                    _continueButton.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1);
                                                });
                                        });
                                });
                        });
                });
        });
}

    private void TryPlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            MusicController.Instance.PlaySpecificSound(clip);
        }
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
        _voltTextTMP.text = "";
        _voltPanel.DOScale(Vector3.zero, 0f)
            .OnStart((() =>
            {
                _voltPanel.gameObject.SetActive(true);
            }))
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
            _voltTextTMP.text = ($"Current volt: {_voltage}");
            _currentVoltText.text = ($"{_voltage}");
        }
    }

    public void TakeVolt()
    {
        if (_voltage > 1)
        {
            _voltage--;
            _voltTextTMP.text = ($"Current volt: {_voltage}");
            _currentVoltText.text = ($"{_voltage}");
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
    
    public void PopupVoltTooLowPanel()
    {
        _voltTooLowPanel.DOKill();
        
        if (_voltTooLowPanel.anchoredPosition.y > -10f)
        {
            _voltTooLowPanel.localScale = Vector3.one;
            _voltTooLowPanel.DOPunchScale(Vector3.one * 0.25f, 0.3f, 10, 1)
                .OnComplete((() =>
                {
                    if (_voltTooLowPanel.localScale.x < 1 && _voltTooLowPanel.localScale.y < 1)
                    {
                        _voltTooLowPanel.DOScale(Vector3.one, 0.3f);
                    }
                }));
        }
        
        _voltTooLowPanel.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.InOutBack)
            .OnComplete((() =>
            {
                _voltTooLowPanel.DOAnchorPosY(-240f, 0.5f)
                    .SetDelay(0.5f)
                    .SetEase(Ease.InOutBack);
            }));
    }
}
