using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeverListController : MonoBehaviour
{
    public static LeverListController Instance;
    
    [SerializeField] private int _starsCount;
    public int _gemsCount;

    [SerializeField] private TextMeshProUGUI _starsText;
    [SerializeField] private TextMeshProUGUI _gemsText;
    
    [SerializeField] private GameObject _notEnoughMoneyPanel;
    [SerializeField] private GameObject _buyLevelPanel;
    [SerializeField] private List<Button> _buttons;

    private Button _activeButton;
    
    private void Awake()
    {
        Instance = this;
        
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }

        for (int i = 0; i < _buttons.Count; i++)
        {
            foreach (Transform child in _buttons[i].transform.GetComponentsInChildren<Transform>(true))
            {
                if (child.CompareTag("LevelText"))
                {
                    TextMeshProUGUI tmpText = child.GetComponent<TextMeshProUGUI>();
                    if (tmpText != null)
                    {
                        tmpText.text = (i + 1).ToString();
                    }
                    break;
                }
            }
        }
        
        GetStarsAndGems();
    }

    private void Start()
    {
        Transform[] children = _buttons[0].GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.CompareTag("Unlock"))
            {
                child.gameObject.SetActive(false);
                break;
            }
        }

        _buttons[0].interactable = true;
        
        GetLevels();
    }

    private void Update()
    {
        _starsText.text = _starsCount.ToString();
        _gemsText.text = _gemsCount.ToString();
    }

    public void OpenBuyLevelPanel(Button button)
    {
        _buyLevelPanel.gameObject.SetActive(true);
        _activeButton = button;
    }

    public void BuyLevelStars()
    {
        if (_starsCount >= 3)
        {
            _starsCount -= 3;
            Transform[] children = _activeButton.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.CompareTag("Unlock"))
                {
                    child.gameObject.SetActive(false);
                    break;
                }
            }
            PlayerPrefs.SetInt($"{_buttons.IndexOf(_activeButton)}", 1);
            _buttons[_buttons.IndexOf(_activeButton)].interactable = true;
            _activeButton = null;
        }
        else
        {
            _notEnoughMoneyPanel.SetActive(true);
        }

        SaveStarsAndGems();
    }

    public void BuyLevelGems()
    {
        if (_gemsCount >= 5)
        {
            _gemsCount -= 5;
            Transform[] children = _activeButton.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.CompareTag("Unlock"))
                {
                    child.gameObject.SetActive(false);
                    break;
                }
            }
            PlayerPrefs.SetInt($"{_buttons.IndexOf(_activeButton)}", 1);
            _buttons[_buttons.IndexOf(_activeButton)].interactable = true;
            _activeButton = null;
        }
        else
        {
            _notEnoughMoneyPanel.SetActive(true);
        }

        SaveStarsAndGems();
    }

    public void CancelBuyLevel()
    {
        _buyLevelPanel.gameObject.SetActive(false);
        _activeButton = null;
    }

    private void GetStarsAndGems()
    {
        _starsCount = PlayerPrefs.GetInt("stars", 0);
        _gemsCount = PlayerPrefs.GetInt("gems", 0);
    }
    private void SaveStarsAndGems()
    {
        PlayerPrefs.SetInt("stars", _starsCount);
        PlayerPrefs.SetInt("gems", _gemsCount);
        PlayerPrefs.Save();
    }

    private void GetLevels()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (PlayerPrefs.HasKey($"i"))
            {
                Transform[] children = _buttons[i].GetComponentsInChildren<Transform>(true);
                _buttons[i].interactable = true;
                foreach (Transform child in children)
                {
                    if (child.CompareTag("Unlock"))
                    {
                        child.gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
