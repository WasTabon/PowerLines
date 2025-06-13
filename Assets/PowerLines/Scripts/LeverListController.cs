using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeverListController : MonoBehaviour
{
    [SerializeField] private int _starsCount;
    [SerializeField] private int _gemsCount;

    [SerializeField] private TextMeshProUGUI _starsText;
    [SerializeField] private TextMeshProUGUI _gemsText;
    
    [SerializeField] private GameObject _buyLevelPanel;
    [SerializeField] private List<Button> _buttons;

    private Button _activeButton;
    
    private void Awake()
    {
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
            _activeButton = null;
        }
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
            _activeButton = null;
        }
    }

    private void GetStarsAndGems()
    {
        _starsCount = PlayerPrefs.GetInt("stars", 0);
        _gemsCount = PlayerPrefs.GetInt("gems", 0);
    }

    private void GetLevels()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (PlayerPrefs.HasKey($"i"))
            {
                Transform[] children = _buttons[i].GetComponentsInChildren<Transform>(true);
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
