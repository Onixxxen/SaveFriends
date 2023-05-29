public class MenuPlayerPresenter
{
    private Player _player;
    private MenuPlayer _menuPlayer;
    private MenuPlayerView _menuPlayerView;

    public void Init(MenuPlayer menuPlayer, MenuPlayerView menuPlayerView, Player player)
    {
        _menuPlayer = menuPlayer;
        _menuPlayerView = menuPlayerView;
        _player = player;
    }

    public void Enable()
    {
        _menuPlayerView.OnGetTotalData += TryGetTotalData;
        _menuPlayerView.OnStartGameButton += TryStartGameRequest;

        _menuPlayer.OnActivateStartMenu += TryActivateStartMenu;
        _menuPlayer.OnGiveTotalData += TryGiveTotalData;
        _menuPlayer.OnRemoveMoney += TryRemoveMoney;

        _player.OnStartGame += TryStartGame;
    }

    public void Disable()
    {
        _menuPlayerView.OnGetTotalData -= TryGetTotalData;
        _menuPlayerView.OnStartGameButton -= TryStartGameRequest;

        _menuPlayer.OnActivateStartMenu -= TryActivateStartMenu;
        _menuPlayer.OnGiveTotalData -= TryGiveTotalData;
        _menuPlayer.OnRemoveMoney -= TryRemoveMoney;

        _player.OnStartGame -= TryStartGame;
    }

    public void TryGetTotalData()
    {
        _menuPlayer.GiveTotalData();
    }

    public void TryGiveTotalData(int heart, int money, int score, int scoreBonus, int moneyBonus)
    {
        _menuPlayerView.SetTotalData(heart, money, score, scoreBonus, moneyBonus);
    }

    public void TryActivateStartMenu(int score, int money)
    {
        _menuPlayerView.ActivateMainMenu(score, money);
    }

    public void TryStartGameRequest()
    {
        _player.ResetValue();
    }

    public void TryStartGame(int heart, int money, int score)
    {
        _menuPlayerView.ActivateGame(heart, money, score);
    }

    public void TryRemoveMoney(int totalMoney)
    {
        _menuPlayerView.RmoveMoney(totalMoney);
    }
}
