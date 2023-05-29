using DG.Tweening;
using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private float _duration;
    private int _reward;
    private float _normalDuration;

    private Transform _startPoint;
    private Transform _wallRight;
    private Transform _wallLeft;
    private Transform _target;
    private FriendsSound _friendsSound;

    public float Duration => _duration;

    public event Action OnRedZoneEnter;
    public event Action<int> OnVentilationEnter;

    public void SetCharacteristics(FriendsSound friendsSound, float duration, int reward)
    {
        _friendsSound = friendsSound;
        _duration = duration;
        _reward = reward;
        _normalDuration = _duration;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetStartPoint(Transform startPoint)
    {
        _startPoint = startPoint;
    }

    public void SetWalls(Transform wallRight, Transform wallLeft)
    {
        _wallLeft = wallLeft;
        _wallRight = wallRight;
    }

    private void OnEnable()
    {
        if (_startPoint != null)
        {
            transform.position = _startPoint.position;
            transform.DOMoveY(_target.position.y, _duration);
        }
    }

    private void OnMouseDown()
    {
        transform.DOPause();
        _friendsSound.FriendsClick.Play();
        FindClosestWall();
    }

    private void OnDisable()
    {
        if (_startPoint != null)
        {
            transform.DOPause();
            transform.position = _startPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RedZone redZone))
        {
            OnRedZoneEnter?.Invoke();
            _friendsSound.FriendsDie.Play();
            gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out Ventilation ventilation))
        {
            OnVentilationEnter?.Invoke(_reward);
            //_friendsSound.FriendsSave.Play();
            //TryShowSupportingText();
            gameObject.SetActive(false);
        }
    }

    private void FindClosestWall()
    {
        float toLeftWall = Vector2.Distance(transform.position, _wallLeft.position);
        float toRightWall = Vector2.Distance(transform.position, _wallRight.position);

        if (toLeftWall <= toRightWall)
            transform.DOMoveX(_wallLeft.position.x, 1);
        else
            transform.DOMoveX(_wallRight.position.x, 1);
    }

    public void RemoveDuration(float duration)
    {
        _duration -= duration;
        //Debug.Log($"Duration: {_duration}");
    }

    public void BackDuration()
    {
        _duration = _normalDuration;
    }

    /*
    public void TryShowSupportingText()
    {
        int randShow = UnityEngine.Random.Range(0, 10);

        if (randShow == 1)
            _enemyPool.SupportingTextView.ShowSupportingText();
    }
    */
}
