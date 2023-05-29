using System.Collections.Generic;
using UnityEngine;

public class BackgroundPresenter
{
    private BackgroundView _backgroundView;
    private Background _background;

    public void Init(BackgroundView backgroundView, Background background)
    {
        _backgroundView = backgroundView;
        _background = background;
    }

    public void Enable()
    {
        _background.OnChangeBackground += TryChangeBackground;
        _background.OnLockButton += TryLockButton;
        _background.OnUnlockButton += TryUnlockButton;

        _backgroundView.OnBackgroundChange += RequestChangeBackground;
        _backgroundView.OnTryLockButton += RequestLockButton;
        _backgroundView.OnTryUnlockButton += RequestUnlockButton;
    }

    public void Disable()
    {
        _background.OnChangeBackground -= TryChangeBackground;
        _background.OnLockButton -= TryLockButton;
        _background.OnUnlockButton -= TryUnlockButton;

        _backgroundView.OnBackgroundChange -= RequestChangeBackground;
        _backgroundView.OnTryLockButton -= RequestLockButton;
        _backgroundView.OnTryUnlockButton -= RequestUnlockButton;
    }

    private void RequestChangeBackground(int cost, int currentBackgroundNumber, int totalBackgroundsCount)
    {
        _background.ChangeBackground(cost, currentBackgroundNumber, totalBackgroundsCount);
    }

    private void RequestLockButton(int cost)
    {
        _background.TryLockButton(cost);
    }

    private void RequestUnlockButton(int cost)
    {
        _background?.TryUnlockButton(cost);
    }

    private void TryChangeBackground(int currentBackgroundNumber)
    {
        _backgroundView.ChangeBackground(currentBackgroundNumber);
    }

    private void TryLockButton()
    {
        _backgroundView.LockButton();
    }

    private void TryUnlockButton()
    {
        _backgroundView.UnlockButton();
    }
}

