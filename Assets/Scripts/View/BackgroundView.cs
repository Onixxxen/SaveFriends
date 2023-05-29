using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private List<Backgrounds> _backgrounds;
    [SerializeField] private Image _gameBackground;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TMP_Text _currentBackgroundNumber;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private GameObject _effect;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private SaverData _saverData;

    private int _currentBackground = 0;
    private int _nextBackground = 1;

    public List<Backgrounds> Backgrounds => _backgrounds;

    public event Action<int, int, int> OnBackgroundChange;
    public event Action<int> OnTryUnlockButton;
    public event Action<int> OnTryLockButton;

    private void OnEnable()
    {
        OnTryUnlockButton?.Invoke(_backgrounds[_nextBackground].Cost);
    }

    public void Init()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeButtonClick);
        //_nextBackground = _currentBackground + 1;

        _gameBackground.sprite = _backgrounds[_currentBackground].Background;
        _buttonImage.sprite = _backgrounds[_nextBackground].Background;
        _costText.text = _backgrounds[_nextBackground].Cost.ToString();
        _currentBackgroundNumber.text = $"{_currentBackground}/{_backgrounds.Count - 1}";

        TryLockButton();
    }

    private void ChangeButtonClick()
    {
        int cost = Convert.ToInt32(_costText.text);
        int currentBackgroundNumber = Convert.ToInt32(_currentBackground);

        Instantiate(_effect, transform.position, Quaternion.identity);
        _sound.Play();

        OnBackgroundChange?.Invoke(cost, currentBackgroundNumber, _backgrounds.Count - 1);
    }

    public void ChangeBackground(int currentBackgroundNumber)
    {
        _currentBackground = currentBackgroundNumber;
        _nextBackground = _currentBackground + 1;

        if (_nextBackground > _backgrounds.Count - 1)
        {
            _nextBackground = 0;

            for (int i = 0; i < _backgrounds.Count; i++)
            {
                _backgrounds[i].Cost = 1;
                _saverData.SaveCostBackgrounds(i, _backgrounds[i].Cost);
            }
        }

        _gameBackground.sprite = _backgrounds[_currentBackground].Background;
        _buttonImage.sprite = _backgrounds[_nextBackground].Background;
        _costText.text = _backgrounds[_nextBackground].Cost.ToString();
        _currentBackgroundNumber.text = $"{_currentBackground}/{_backgrounds.Count - 1}";

        _saverData.SaveCurrentBackground(_currentBackground);
        _saverData.SaveNextBackground(_nextBackground);

        TryLockButton();
    }

    public void TryLockButton()
    {
        OnTryLockButton?.Invoke(_backgrounds[_nextBackground].Cost);
    }

    public void LockButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void UnlockButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void LoadBackground()
    {
        _currentBackground = YandexGame.savesData.SavedCurrentBackground;
        _nextBackground = YandexGame.savesData.SavedNextBackground;

        Init();
    }

    public void LoadCost(int index)
    {
        _backgrounds[index].Cost = YandexGame.savesData.SavedCostBackgrounds[index];
    }
}
