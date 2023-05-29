using System;

public class Background
{
    private MenuPlayer _menuPlayer;

    public event Action<int> OnChangeBackground;
    public event Action OnLockButton;
    public event Action OnUnlockButton;

    public Background(MenuPlayer menuPlayer)
    {
        _menuPlayer = menuPlayer;
    }

    public void ChangeBackground(int cost, int currentBackgroundNumber, int totalBackgroundsCount)
    {
        if (currentBackgroundNumber < totalBackgroundsCount)
            currentBackgroundNumber++;
        else
            currentBackgroundNumber = 0;

        _menuPlayer.RemoveMoney(cost);
        OnChangeBackground?.Invoke(currentBackgroundNumber);
    }

    public void TryLockButton(int cost)
    {
        if (_menuPlayer.TotalMoney < cost)
            OnLockButton?.Invoke();
    }

    public void TryUnlockButton(int cost)
    {
        if (_menuPlayer.TotalMoney > cost)
            OnUnlockButton?.Invoke();
    }
}
