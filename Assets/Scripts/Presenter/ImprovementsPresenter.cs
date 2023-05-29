using System.Collections.Generic;
using UnityEngine;

public class ImprovementsPresenter
{
    private Improvements _improvements;
    private List<ImprovementsView> _improvementsView = new List<ImprovementsView>();    
    private MenuPlayer _menuPlayer;
    private MenuPlayerView _menuPlayerView;

    public void Init(Improvements improvements, List<ImprovementsView> improvementsView, MenuPlayer menuPlayer, MenuPlayerView menuPlayerView)
    {
        _improvements = improvements;
        _menuPlayer = menuPlayer;
        _menuPlayerView = menuPlayerView;

        for (int i = 0; i < improvementsView.Count; i++)
            _improvementsView.Add(improvementsView[i]);
    }

    public void Enable()
    {
        for (int i = 0; i < _improvementsView.Count; i++)
        {
            _improvementsView[i].OnImproveButtonClick += TryImprove;
            _improvementsView[i].OnTryUnlockImprove += CheckEnoughMoney;
            _improvementsView[i].OnTryLockImprove += CheckEnoughMoney;
        }

        _menuPlayer.OnChangeValues += TryChangeValues;
        _menuPlayer.OnLowMoney += TryLockImprove;
        _menuPlayer.OnEnoughMoney += TryUnlockImprove;
    }

    public void Disable()
    {
        for (int i = 0; i < _improvementsView.Count; i++)
        {
            _improvementsView[i].OnImproveButtonClick -= TryImprove;
            _improvementsView[i].OnTryUnlockImprove -= CheckEnoughMoney;
            _improvementsView[i].OnTryLockImprove -= CheckEnoughMoney;
        }

        _menuPlayer.OnChangeValues -= TryChangeValues;
        _menuPlayer.OnLowMoney -= TryLockImprove;
        _menuPlayer.OnEnoughMoney -= TryUnlockImprove;
    }

    public void TryImprove(ImprovementsView improvementsView, int cost, int buyed, int improveHeart, int improveMoney, int improveSocore)
    {
        _improvements.Improve(improvementsView, cost, buyed, improveHeart, improveMoney, improveSocore);
    }

    public void TryChangeValues(ImprovementsView improvementsView, int cost, int buyed, int totalHeart, int moneyBonus, int scoreBonus, int totalMoney)
    {
        improvementsView.ChangeValues(cost, buyed);
        _menuPlayerView.ChangeValues(totalHeart, moneyBonus, scoreBonus, totalMoney);
    }

    public void TryLockImprove(ImprovementsView improvementsView)
    {
        improvementsView.LockImprove();
    }

    public void CheckEnoughMoney(ImprovementsView improvementsView, int cost)
    {
        _menuPlayer.CheckEnoughMoney(improvementsView, cost);
    }

    public void TryUnlockImprove(ImprovementsView improvementsView)
    {
        improvementsView.UnlockImprove();
    }
}
