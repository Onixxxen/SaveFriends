using UnityEngine;
using YG;

public class RewardView : MonoBehaviour
{
    [SerializeField] private LoseGameView _loseGameView;

    private void OnEnable() => YandexGame.RewardVideoEvent += OnRewardEvent;
    private void OnDisable() => YandexGame.RewardVideoEvent -= OnRewardEvent;

    private void OnRewardEvent(int id)
    {
        if (id == 1)
            _loseGameView.ContinueGame();
    }    
}
