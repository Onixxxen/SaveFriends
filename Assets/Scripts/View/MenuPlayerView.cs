using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuPlayerView : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private TMP_Text _totalHeart;
    [SerializeField] private TMP_Text _totalMoney;
    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private TMP_Text _scoreBonus;
    [SerializeField] private TMP_Text _moneyBonus;
    [SerializeField] private Button _startButton;
    [SerializeField] private BladeRotate _bladeRotate;
    [SerializeField] private SaverData _saver;

    public event Action OnGetTotalData;
    public event Action OnStartGameButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGameButton);
        OnGetTotalData?.Invoke();
    }

    public void StartGame()
    {
        _enemySpawner.ChangeSecondBetweenSpawn(10000);
        _playerView.ChangeIsAlive(false);
        _menuCanvas.SetActive(true);
    }

    public void GetTotalData()
    {
        OnGetTotalData?.Invoke();
    }

    public void SetTotalData(int heart, int money, int score, int scoreBonus, int moneyBonus)
    {
        _totalHeart.text = heart.ToString();
        _totalMoney.text = money.ToString();
        _totalScore.text = score.ToString();
        _scoreBonus.text = scoreBonus.ToString();
        _moneyBonus.text = moneyBonus.ToString();
    }

    public void ActivateMainMenu(int score, int money)
    {
        _totalScore.text = score.ToString();
        _totalMoney.text = money.ToString();

        _saver.SaveScore(score);
        _saver.SaveMoney(money);

        _menuCanvas.SetActive(true);
    }

    public void StartGameButton()
    {
        OnStartGameButton?.Invoke();
    }

    public void ActivateGame(int heart, int money, int score)
    {
        _playerView.UpdateHeart(heart);
        _playerView.UpdateMoney(money);
        _playerView.UpdateScore(score);

        for (int i = 0; i < _enemySpawner.Pool.Count; i++)
            _enemySpawner.Pool[i].BackDuration();

        _bladeRotate.StartRotate();
        _gameUICanvas.SetActive(true);
        _enemySpawner.ChangeSecondBetweenSpawn(2);
        _playerView.ChangeIsAlive(true);
        _menuCanvas.SetActive(false);
    }

    public void ChangeValues(int totalHeart, int moneyBonus, int scoreBonus, int totalMoney)
    {
        _totalHeart.text = totalHeart.ToString();
        _moneyBonus.text = moneyBonus.ToString();
        _scoreBonus.text = scoreBonus.ToString();
        _totalMoney.text = totalMoney.ToString();

        _saver.SaveMenuPlayerData(totalHeart, totalMoney, scoreBonus, moneyBonus);
    }

    public void RmoveMoney(int totalMoney)
    {
        _totalMoney.text = totalMoney.ToString();

        _saver.SaveMoney(totalMoney);
    }
}
