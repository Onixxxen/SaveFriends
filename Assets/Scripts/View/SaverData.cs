using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

public class SaverData : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private List<ImprovementsView> _improvementsViews;
    [SerializeField] private SettingLanguageView _settingLanguageView;
    [SerializeField] private BackgroundView _backgroundView;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void SaveMenuPlayerData(int totalHeart, int totalMoney, int scoreBonus, int moneyBonus)
    {
        YandexGame.savesData.SavedTotalHeart = totalHeart;
        YandexGame.savesData.SavedTotalMoney = totalMoney;
        YandexGame.savesData.SavedScoreBonus = scoreBonus;
        YandexGame.savesData.SavedMoneyBonus = moneyBonus;
        YandexGame.SaveProgress();
    }

    public void SaveScore(int score)
    {
        YandexGame.savesData.SavedTotalScore = score;
        YandexGame.SaveProgress();
    }

    public void SaveMoney(int money)
    {
        YandexGame.savesData.SavedTotalMoney = money;
        YandexGame.SaveProgress();
    }

    public void SaveImproveData(int index, int cost, int buyed)
    {
        YandexGame.savesData.SavedCostImprove[index] = cost;
        YandexGame.savesData.SavedBuyedImprove[index] = buyed;
        YandexGame.SaveProgress();
    }

    public void SaveGoal(int goal)
    {
        YandexGame.savesData.SavedGoal = goal;
        YandexGame.SaveProgress();
    }

    public void SaveCurrentBackground(int number)
    {
        YandexGame.savesData.SavedCurrentBackground = number;
        YandexGame.SaveProgress();
    }

    public void SaveNextBackground(int number)
    {
        YandexGame.savesData.SavedNextBackground = number;
        YandexGame.SaveProgress();
    }

    public void SaveCostBackgrounds(int index, int cost)
    {
        YandexGame.savesData.SavedCostBackgrounds[index] = cost;
        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        if (!YandexGame.savesData.IsLanguageLoaded)
            _settingLanguageView.CheckYandexLanguage();
        else
            _settingLanguageView.LoadLanguageData();        

        _settingLanguageView.SettingPanel.SetActive(false);        

        _gameController.MenuPlayer.LoadMenuPlayerData();
        _gameController.Goal.LoadData();
                
        for (int i = 0; i < _improvementsViews.Count; i++)
            if (YandexGame.savesData.SavedCostImprove[i] != 0)
                _improvementsViews[i].LoadData(i);
         
        for (int i = 0; i < _backgroundView.Backgrounds.Count; i++)
            if (YandexGame.savesData.SavedCostBackgrounds[i] != 0)
                _backgroundView.LoadCost(i);

        _backgroundView.LoadBackground();
    }
}
