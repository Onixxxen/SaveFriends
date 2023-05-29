public class EnemyPresenter
{
    private EnemySpawner _enemySpawner;
    private Enemy _enemy;
    private PlayerView _playerView;

    public void Init(EnemySpawner enemySpawner, Enemy enemy, PlayerView playerView)
    {
        _enemySpawner = enemySpawner;
        _enemy = enemy;
        _playerView = playerView;
    }

    public void Enable()
    {
        _enemySpawner.OnRedZoneEnter += EnemyEnterRedZone;
        _enemySpawner.OnVentilationEnter += EnemyEnterVentilation;

        _enemy.OnChangeEnemy += TryChangeEnemy;

        _playerView.OnChangeTime += ChangeEnemyRequest;
    }

    public void Disable()
    {
        _enemySpawner.OnRedZoneEnter -= EnemyEnterRedZone;
        _enemySpawner.OnVentilationEnter -= EnemyEnterVentilation;

        _enemy.OnChangeEnemy += TryChangeEnemy;

        _playerView.OnChangeTime -= ChangeEnemyRequest;
    }

    public void EnemyEnterRedZone()
    {
        _enemy.RemovePlayerHeart();
    }

    public void EnemyEnterVentilation(int reward)
    {
        _enemy.AddPlayerMoney(reward);
    }

    public void ChangeEnemyRequest()
    {
        _enemy.ChangeEnemy();
    }

    public void TryChangeEnemy()
    {

        for (int i = 0; i < _enemySpawner.Pool.Count; i++)
        {
            if (_enemySpawner.Pool[i].Duration >= 4)
                _enemySpawner.Pool[i].RemoveDuration(0.0001f);
        }

        if (_enemySpawner.SecondsBetweenSpawn >= 0.5)
            _enemySpawner.RemoveSecondBetweenSpawn(0.0001f);
    }
}
