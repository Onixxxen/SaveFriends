using System;
using YG;

public class MenuPlayer
{
    private int _totalHeart = 3;
    private int _totalMoney = 500;
    private int _totalScore = 0;
    private int _scoreBonus = 1;
    private int _moneyBonus = 1;

    public int TotalHeart => _totalHeart;
    public int TotalMoney => _totalMoney;   
    public int ScoreBonus => _scoreBonus;
    public int MoneyBonus => _moneyBonus;

    public event Action<int, int, int, int, int> OnGiveTotalData;
    public event Action<int, int> OnActivateStartMenu;
    public event Action<ImprovementsView, int, int, int, int, int, int> OnChangeValues;
    public event Action<int> OnRemoveMoney;
    public event Action<ImprovementsView> OnLowMoney;
    public event Action<ImprovementsView> OnEnoughMoney;

    public void GiveTotalData()
    {
        OnGiveTotalData?.Invoke(_totalHeart, _totalMoney, _totalScore, _scoreBonus, _moneyBonus);
    }

    public void ActivateStartMenu(int score, int money)
    {
        if (score > _totalScore)
        {
            _totalScore = score;
            YandexGame.NewLeaderboardScores("score", _totalScore);
        }

        _totalMoney += money;

        OnActivateStartMenu?.Invoke(_totalScore, _totalMoney);
    }

    public void ChangeValues(ImprovementsView improvementsView, int cost, int buyed, int improveHeart, int improveMoney, int improveSocore)
    {
        _totalHeart += improveHeart;
        _moneyBonus += improveMoney;
        _scoreBonus += improveSocore;
        _totalMoney -= cost;

        cost *= 2;
        buyed++;

        OnChangeValues?.Invoke(improvementsView, cost, buyed, _totalHeart, _moneyBonus, _scoreBonus, _totalMoney);
    }

    public void RemoveMoney(int cost)
    {
        _totalMoney -= cost;
        OnRemoveMoney?.Invoke(_totalMoney);
    }

    public void CheckEnoughMoney(ImprovementsView improvementsView, int cost)
    {
        if (_totalMoney >= cost)
            OnEnoughMoney?.Invoke(improvementsView);
        else
            OnLowMoney?.Invoke(improvementsView);
    }

    public void LoadMenuPlayerData()
    {
        _totalHeart = YandexGame.savesData.SavedTotalHeart;
        _totalMoney = YandexGame.savesData.SavedTotalMoney;
        _totalScore = YandexGame.savesData.SavedTotalScore;
        _scoreBonus = YandexGame.savesData.SavedScoreBonus;
        _moneyBonus = YandexGame.savesData.SavedMoneyBonus;

        GiveTotalData();
    }
}
