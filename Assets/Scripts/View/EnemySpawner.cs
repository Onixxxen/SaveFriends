using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyPool
{
    [SerializeField] private List<Enemies> _enemies = new List<Enemies>();
    [SerializeField] private Transform[] _spawnPoints;    
    [SerializeField] private float _secondsBetweenSpawn;

    private float _previousSecondBetweenSpawn;
    private float _elapsedTime;
    public float SecondsBetweenSpawn => _secondsBetweenSpawn;

    private void Start()
    {
        for (int i = 0; i < _enemies.Count; i++)
            Initialize(_enemies[i], _enemies[i].Template);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out EnemyView enemy))
            {
                _elapsedTime = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                enemy.SetStartPoint(_spawnPoints[spawnPointNumber]);
                SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetEnemy(EnemyView enemy, Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
    }

    public void ChangeSecondBetweenSpawn(float newValue)
    {
        _previousSecondBetweenSpawn = _secondsBetweenSpawn;
        _secondsBetweenSpawn = newValue;
    }

    public void BackSecondBetweenSpawn()
    {
        _secondsBetweenSpawn = _previousSecondBetweenSpawn;
    }

    public void RemoveSecondBetweenSpawn(float value)
    {
        _secondsBetweenSpawn -= value;
        //Debug.Log($"SecondsBetweenSpawn: {_secondsBetweenSpawn}");
    }
}
