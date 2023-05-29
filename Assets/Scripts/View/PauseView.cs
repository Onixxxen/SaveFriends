using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private SoundSettingView _soundSettingView;

    private bool _isPause;

    public bool IsPause => _isPause;
    public SoundSettingView SoundSettingView => _soundSettingView;

    public void Pause(bool isActive)
    {
        if (isActive)
        {
            Time.timeScale = 0;
            _playerView.ChangeIsAlive(false);

            _isPause = true;
        }
        else
        {
            Time.timeScale = 1;
            _playerView.ChangeIsAlive(true);

            _isPause = false;
        }
    }
}
