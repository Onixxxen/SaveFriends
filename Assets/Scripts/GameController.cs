using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private LoseGameView _loseGameView;
    [SerializeField] private MenuPlayerView _menuPlayerView;
    [SerializeField] private GoalView _goalView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private List<ImprovementsView> _improvementsView;
    [SerializeField] private BackgroundView _backgroundView;

    private EnemyPresenter _enemyPresenter;
    private PlayerPresenter _playerPresenter;
    private LoseGamePresenter _loseGamePresenter;
    private MenuPlayerPresenter _menuPlayerPresenter;
    private ImprovementsPresenter _improvementsPresenter;
    private GoalPresenter _goalPresenter;
    private BackgroundPresenter _backgroundPresenter;

    private MenuPlayer _menuPlayer;
    private Player _player;
    private Enemy _enemy;
    private Improvements _improvements;
    private Goal _goal;
    private Background _background;

    public MenuPlayer MenuPlayer => _menuPlayer;
    public Goal Goal => _goal;

    private void OnEnable()
    {
        _enemyPresenter.Enable();
        _playerPresenter.Enable();
        _loseGamePresenter.Enable();
        _menuPlayerPresenter.Enable();
        _improvementsPresenter.Enable();
        _goalPresenter.Enable();
        _backgroundPresenter.Enable();
    }

    private void OnDisable()
    {
        _enemyPresenter.Disable();
        _playerPresenter.Disable();
        _loseGamePresenter.Disable();
        _menuPlayerPresenter.Disable();
        _improvementsPresenter.Disable();
        _goalPresenter.Disable();
        _backgroundPresenter.Disable();
    }

    private void Awake()
    {
        _menuPlayer = new MenuPlayer();
        _player = new Player(_menuPlayer);
        _enemy = new Enemy(_player);
        _improvements = new Improvements(_menuPlayer);
        _goal = new Goal(_player);
        _background = new Background(_menuPlayer);

        _enemyPresenter = new EnemyPresenter();
        _playerPresenter = new PlayerPresenter();
        _loseGamePresenter = new LoseGamePresenter();
        _menuPlayerPresenter = new MenuPlayerPresenter();
        _improvementsPresenter = new ImprovementsPresenter();
        _goalPresenter = new GoalPresenter();
        _backgroundPresenter = new BackgroundPresenter();

        _enemyPresenter.Init(_enemySpawner, _enemy, _playerView);
        _playerPresenter.Init(_player, _playerView, _loseGameView, _goal);
        _loseGamePresenter.Init(_player, _menuPlayer, _loseGameView);
        _menuPlayerPresenter.Init(_menuPlayer, _menuPlayerView, _player);
        _improvementsPresenter.Init(_improvements, _improvementsView, _menuPlayer, _menuPlayerView);
        _goalPresenter.Init(_goalView, _goal);
        _backgroundPresenter.Init(_backgroundView, _background);

        _menuPlayerView.StartGame();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && !_pauseView.IsPause)
        {
            _pauseView.SoundSettingView.UpdateSoundVolume();
            _pauseView.SoundSettingView.PauseSound();
            _pauseView.Pause(true);
        }
        else
        {
            _pauseView.SoundSettingView.BackSound();
            _pauseView.Pause(false);
        }
    }
}
