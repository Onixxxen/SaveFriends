using TMPro;
using UnityEngine;

public class TranslateView : MonoBehaviour
{
    [Multiline]
    [SerializeField] private string _ruText;
    [Multiline]
    [SerializeField] private string _enText;

    public void SetRussianLanguage()
    {
        GetComponent<TMP_Text>().text = _ruText;
    }

    public void SetEnglishLanguage()
    {
        GetComponent<TMP_Text>().text = _enText;
    }
}
