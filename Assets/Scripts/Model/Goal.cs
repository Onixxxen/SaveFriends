using System;
using YG;

public class Goal
{
    private Player _player;

    private int _goal = 300000;

    public event Action<int> OnGiveGoal;
    public event Action<int> OnUpdateGoalBar;
    public event Action OnCompleteGoal;

    public Goal(Player player)
    {
        _player = player;
    }

    public void GiveGoal()
    {
        OnGiveGoal?.Invoke(_goal);
    }

    public void UpdateGoalBar()
    {
        if (_player.Score < _goal)
            OnUpdateGoalBar?.Invoke(_player.Score);
        else if (_player.Score >= _goal)
            CompleteGoal();
    }

    public void CompleteGoal()
    {
        _player.AddMoney(_goal / 10);
        _player.RestoreHeart();
        _goal *= 2;

        OnCompleteGoal?.Invoke();
    }

    public void LoadData()
    {
        _goal = YandexGame.savesData.SavedGoal;
    }
}
