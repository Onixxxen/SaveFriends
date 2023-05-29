using System;
using UnityEngine;

public class Enemy
{
    private Player _player;

    public event Action OnChangeEnemy;

    public Enemy(Player player)
    {
        _player = player;
    }

    public void RemovePlayerHeart()
    {
        _player.RemoveHeart();
    }

    public void AddPlayerMoney(int reward)
    {
        _player.AddMoney(reward);
    }

    public void ChangeEnemy()
    {
        OnChangeEnemy?.Invoke();
    }
}
