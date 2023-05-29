using System;

public class Player
{
    private int _heart = 5;
    private int _money = 0;
    private int _score = 0;

    private MenuPlayer _menuPlayer;

    public int Score => _score;

    public event Action<int> OnRemoveHeart;
    public event Action<int> OnGiveHeart;
    public event Action<int> OnAddMoney;
    public event Action<int> OnGiveMoney;
    public event Action<int> OnChangeScore;
    public event Action<int, int> OnLoseGame;
    public event Action<int, int, int> OnStartGame;

    public Player(MenuPlayer menuPlayer)
    {
        _menuPlayer = menuPlayer;
    }

    public void GiveHeart()
    {
        OnGiveHeart?.Invoke(_heart);
    }

    public void RemoveHeart()
    {
        _heart--;
        OnRemoveHeart?.Invoke(_heart);

        if (_heart <= 0)
            OnLoseGame?.Invoke(_score, _money);
    }

    public void GiveMoney()
    {
        OnGiveMoney?.Invoke(_money);
    }

    public void AddMoney(int reward)
    {
        _money += reward * _menuPlayer.MoneyBonus;

        OnAddMoney?.Invoke(_money);
    }

    public void ChangeScore()
    {
        _score += 1 * _menuPlayer.ScoreBonus;

        OnChangeScore?.Invoke(_score);
    }

    public void RestoreHeart()
    {
        _heart = _menuPlayer.TotalHeart;
        OnGiveHeart?.Invoke(_heart);
    }
    
    public void ResetValue()
    {
        _heart = _menuPlayer.TotalHeart;
        _money = 0;
        _score = 0;

        OnStartGame?.Invoke(_heart, _money, _score);
    }
}
