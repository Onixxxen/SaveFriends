using System;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _heart;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _score;

    private bool _isAlive = true;

    public event Action OnGetHeart;
    public event Action OnGetMoney;
    public event Action OnChangeScore;
    public event Action OnChangeTime;

    private void Start()
    {
        GetHeart();
        GetMoney();
    }

    private void Update()
    {
        if (_isAlive)
        {
            OnChangeScore?.Invoke();
            OnChangeTime?.Invoke();
        }
    }

    private void GetHeart()
    {
        OnGetHeart?.Invoke();
    }

    private void GetMoney()
    {
        OnGetMoney?.Invoke();
    }

    public void UpdateHeart(int heart)
    {
        _heart.text = heart.ToString();
    }

    public void UpdateMoney(int money)
    {
        _money.text = money.ToString();
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }

    public void ChangeIsAlive(bool isAlive)
    {
        _isAlive = isAlive;
    }
}
