using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies", menuName = "Enemies/CreateEnemy")]
public class Enemies : ScriptableObject
{
    public EnemyView Template;
    public float Duration;
    public int Reward;
}
