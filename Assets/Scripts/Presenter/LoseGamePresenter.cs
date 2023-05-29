using UnityEngine;

public class LoseGamePresenter
{
    private Player _player;
    private MenuPlayer _menuPlayer;
    private LoseGameView _loseGameView;

    public void Init(Player player, MenuPlayer menuPlayer, LoseGameView loseGameView)
    {
        _player = player;
        _menuPlayer = menuPlayer;
        _loseGameView = loseGameView;
    }

    public void Enable()
    {
        _player.OnLoseGame += TryLoseGame;

        _loseGameView.OnContinueGame += TryContinueGame;
        _loseGameView.OnActivateStartMenu += TryActivateStartMenu;
    }

    public void Disable()
    {
        _player.OnLoseGame -= TryLoseGame;

        _loseGameView.OnContinueGame -= TryContinueGame;
        _loseGameView.OnActivateStartMenu -= TryActivateStartMenu;
    }

    public void TryLoseGame(int score, int money)
    {
        _loseGameView.LoseGame(score, money);
    }

    public void TryContinueGame()
    {
        _player.RestoreHeart();
    }

    public void TryActivateStartMenu(int score, int money)
    {
        _menuPlayer.ActivateStartMenu(score, money);
    }
}
