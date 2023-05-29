using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform _blades;
    [SerializeField] private Transform _wallRight;
    [SerializeField] private Transform _wallLeft;
    [SerializeField] private FriendsSound _friendSound;
    //[SerializeField] private SupportingTextView _supportingTextView;

    private List<EnemyView> _pool = new List<EnemyView>();

    public List<EnemyView> Pool => _pool;
    //public SupportingTextView SupportingTextView => _supportingTextView;

    public event Action OnRedZoneEnter;
    public event Action<int> OnVentilationEnter;

    public void Initialize(Enemies enemy, EnemyView prefab)
    {
        var spawned = Instantiate(prefab, _container.transform);

        spawned.OnRedZoneEnter += EnterRedZone;
        spawned.OnVentilationEnter += EnterVentilation;

        spawned.SetCharacteristics(_friendSound, enemy.Duration, enemy.Reward);
        spawned.SetTarget(_blades);
        spawned.SetWalls(_wallRight, _wallLeft);

        _pool.Add(spawned);

        spawned.gameObject.SetActive(false);
    }

    public bool TryGetObject(out EnemyView result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void EnterRedZone()
    {
        OnRedZoneEnter?.Invoke();
    }

    public void EnterVentilation(int reward)
    {
        OnVentilationEnter?.Invoke(reward);
    }
}
