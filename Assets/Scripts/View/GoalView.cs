using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _goalScoreText;
    [SerializeField] private SaverData _saverData;

    public event Action OnGetGoal;

    private void Start()
    {
        GetGoal();
    }

    public void GetGoal()
    {
        OnGetGoal?.Invoke();
    }

    public void UpdateGoal(int goal)
    {
        _slider.DOValue(_slider.minValue, 1);
        _slider.maxValue = goal;

        _goalScoreText.text = goal.ToString();

        _saverData.SaveGoal(goal);
    }

    public void UpdateGoalBar(int score)
    {
        _slider.DOValue(score, 1);
    }

    public void CompleteGoal()
    {
        GetGoal();
    }
}
