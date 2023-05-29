using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupportingTextView : MonoBehaviour
{
    [SerializeField] private List<SupportingText> _supportingTexts;
    [SerializeField] private TMP_Text _supportingText;
    [SerializeField] private SettingLanguageView _settingLanguage;
    //[SerializeField] private GameObject _effect;

    public void ShowSupportingText()
    {
        if (_supportingText.gameObject.activeInHierarchy)
            _supportingText.gameObject.SetActive(false);

        int textNumber = Random.Range(0, _supportingTexts.Count);

        if (_settingLanguage.CurrentLanguage == "ru")
            _supportingText.text = _supportingTexts[textNumber].RuText;
        else if (_settingLanguage.CurrentLanguage == "en")
            _supportingText.text = _supportingTexts[textNumber].EnText;

        _supportingText.gameObject.SetActive(true);
        //Instantiate(_effect, transform.position, Quaternion.identity);
        StartCoroutine(SupportingTextAnimation());
    }

    private IEnumerator SupportingTextAnimation()
    {
        //_supportingText.GetComponent<Animator>().SetTrigger("TextIsActive");

        //yield return new WaitUntil(AnimationIsEnd);
        yield return new WaitForSeconds(3);
        _supportingText.gameObject.SetActive(false);
    }

    private bool AnimationIsEnd()
    {
        return _supportingText.transform.localScale.x == 0;
    }
}
