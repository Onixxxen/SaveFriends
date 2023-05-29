using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoseGameView : MonoBehaviour
{
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private EnemySpawner _enemySpawner;    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Button _continueButtton;
    [SerializeField] private Slider _slider;
    [SerializeField] private BladeRotate _bladeRotate;

    public event Action OnContinueGame;
    public event Action<int, int> OnActivateStartMenu;

    private void Start()
    {
        _continueButtton.onClick.AddListener(TryContinueGame);
    }

    public void LoseGame(int score, int money)
    {
        gameObject.SetActive(true);
        _enemySpawner.ChangeSecondBetweenSpawn(10000);
        _playerView.ChangeIsAlive(false);

        for (int i = 0; i < _enemySpawner.Pool.Count; i++)
            _enemySpawner.Pool[i].gameObject.SetActive(false);

        _scoreText.text = score.ToString();
        _moneyText.text = money.ToString();

        StartCoroutine(ActivateStartMenu(score, money));
    }

    public void TryContinueGame()
    {
        YandexGame.RewVideoShow(1);
    }

    public void ContinueGame()
    {
        _enemySpawner.BackSecondBetweenSpawn();
        OnContinueGame?.Invoke();
        _playerView.ChangeIsAlive(true);
        gameObject.SetActive(false);
    }

    public IEnumerator ActivateStartMenu(int score, int money)
    {
        _slider.value = _slider.maxValue;
        _slider.DOValue(0, 5);

        yield return new WaitUntil(() => _slider.value == 0);

        OnActivateStartMenu?.Invoke(score, money);
        _bladeRotate.StopRotate();
        gameObject.SetActive(false);
        _gameUICanvas.SetActive(false);
        YandexGame.FullscreenShow();
    }
}
