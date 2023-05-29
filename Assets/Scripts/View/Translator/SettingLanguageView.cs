using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingLanguageView : MonoBehaviour
{
    [Header("Choise Language Button")]
    [SerializeField] private Button _ruLanguageButton;
    [SerializeField] private Button _enLanguageButton;

    [Header("Current Language Panel")]
    [SerializeField] private GameObject _currentLanguagePanel;

    [Header("Setting Panel")]
    [SerializeField] private GameObject _settingPanel;


    private TranslateView[] _translateView;
    private string _currentLanguage = "ru";

    public string CurrentLanguage => _currentLanguage;
    public GameObject SettingPanel => _settingPanel;

    private void Awake()
    {
        _translateView = FindObjectsOfType<TranslateView>(true);

        _ruLanguageButton.onClick.AddListener(SetRussianLanguage);
        _enLanguageButton.onClick.AddListener(SetEnglishLanguage);
    }

    private void SetRussianLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetRussianLanguage();

        ChangeCurrentLanguage(_ruLanguageButton.transform);
        _currentLanguage = "ru";
        SaveLanguage();
    }

    private void SetEnglishLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetEnglishLanguage();

        ChangeCurrentLanguage(_enLanguageButton.transform);
        _currentLanguage = "en";
        SaveLanguage();
    }

    private void ChangeCurrentLanguage(Transform button)
    {
        _currentLanguagePanel.transform.SetParent(button);
        _currentLanguagePanel.transform.position = button.position;
    }
    private void SaveLanguage()
    {
        YandexGame.savesData.SavedLanguage = _currentLanguage;
    }

    public void CheckYandexLanguage()
    {
        if (YandexGame.SDKEnabled)
        {
            if (YandexGame.EnvironmentData.language == "ru")
                SetRussianLanguage();
            else if (YandexGame.EnvironmentData.language == "en")
                SetEnglishLanguage();
            else if (YandexGame.EnvironmentData.language == "tr")

            Debug.Log("СДК успел");

            YandexGame.savesData.IsLanguageLoaded = true;
        }
        else
        {
            Debug.Log("СДК не успел и язык тоже");
        }
    }

    public void LoadLanguageData()
    {
        if (YandexGame.savesData.SavedLanguage == "ru")
            SetRussianLanguage();
        else if (YandexGame.savesData.SavedLanguage == "en")
            SetEnglishLanguage();
    }
}