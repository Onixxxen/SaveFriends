using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ImprovementsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _buyedText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private int _improveHeart;
    [SerializeField] private int _improveMoney;
    [SerializeField] private int _improveScore;
    [SerializeField] private int _cost;
    [SerializeField] private int _buyed;
    [SerializeField] private List<ImprovementsView> _improvementsView;
    [SerializeField] private BackgroundView _backgroundView;
    [SerializeField] private GameObject _effect;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private SaverData _saverData;

    public int Cost => _cost;
    public int Buyed => _buyed;

    public event Action<ImprovementsView, int, int, int, int, int> OnImproveButtonClick;
    public event Action<ImprovementsView, int> OnTryUnlockImprove;
    public event Action<ImprovementsView, int> OnTryLockImprove;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ImproveButtonClick);

        _buyedText.text = _buyed.ToString();
        _costText.text = _cost.ToString();
    }

    private void OnEnable()
    {
        OnTryUnlockImprove?.Invoke(this, _cost);
    }

    public void ImproveButtonClick()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        _sound.Play();
        OnImproveButtonClick?.Invoke(this, _cost, _buyed, _improveHeart, _improveMoney, _improveScore);
    }

    public void ChangeValues(int cost, int buyed)
    {
        _cost = cost;
        _buyed = buyed;

        _buyedText.text = _buyed.ToString();
        _costText.text = _cost.ToString();

        for (int i = 0; i < _improvementsView.Count; i++)
            _saverData.SaveImproveData(i, _improvementsView[i].Cost, _improvementsView[i].Buyed);

        for (int i = 0; i < _improvementsView.Count; i++)
            _improvementsView[i].OnTryLockImprove?.Invoke(_improvementsView[i], _improvementsView[i].Cost);

        _backgroundView.TryLockButton();
    }

    public void LockImprove()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void UnlockImprove()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void LoadData(int index)
    {
        _cost = YandexGame.savesData.SavedCostImprove[index];
        _buyed = YandexGame.savesData.SavedBuyedImprove[index];

        _buyedText.text = _buyed.ToString();
        _costText.text = _cost.ToString();

        for (int i = 0; i < _improvementsView.Count; i++)
            _improvementsView[i].OnTryLockImprove?.Invoke(_improvementsView[i], _improvementsView[i].Cost);
    }
}
