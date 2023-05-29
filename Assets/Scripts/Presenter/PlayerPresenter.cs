public class PlayerPresenter
{
    private Player _player;
    private PlayerView _playerView;
    private LoseGameView _loseGameView;
    private Goal _goal;

    public void Init(Player player, PlayerView playerView, LoseGameView loseGameView, Goal goal)
    {
        _player = player;
        _playerView = playerView;
        _loseGameView = loseGameView;
        _goal = goal;
    }

    public void Enable()
    {
        _player.OnRemoveHeart += TryUpdateHeart;
        _player.OnGiveHeart += TryUpdateHeart;
        _player.OnAddMoney += TryUpdateMoney;
        _player.OnGiveMoney += TryUpdateMoney;
        _player.OnChangeScore += TryUpdateScore;

        _playerView.OnGetHeart += TryGetHeart;
        _playerView.OnGetMoney += TryGetMoney;
        _playerView.OnChangeScore += TryChangeScore;
    }

    public void Disable()
    {
        _player.OnRemoveHeart -= TryUpdateHeart;
        _player.OnGiveHeart -= TryUpdateHeart;
        _player.OnAddMoney -= TryUpdateMoney;
        _player.OnGiveMoney -= TryUpdateMoney;
        _player.OnChangeScore -= TryUpdateScore;

        _playerView.OnGetHeart -= TryGetHeart;
        _playerView.OnGetMoney -= TryGetMoney;
        _playerView.OnChangeScore -= TryChangeScore;
    }

    public void TryGetHeart()
    {
        _player.GiveHeart();
    }

    public void TryUpdateHeart(int heart)
    {
        _playerView.UpdateHeart(heart);
    }

    public void TryGetMoney()
    {
        _player.GiveMoney();
    }

    public void TryUpdateMoney(int money)
    {
        _playerView.UpdateMoney(money);
    }

    public void TryChangeScore()
    {
        _player.ChangeScore();
        _goal.UpdateGoalBar();
    }

    public void TryUpdateScore(int score)
    {
        _playerView.UpdateScore(score);
    }

    public void TryLoseGame(int score, int money)
    {
        _loseGameView.LoseGame(score, money);
    }

    public void TryContinueGame()
    {
        _player.RestoreHeart();
    }
}
