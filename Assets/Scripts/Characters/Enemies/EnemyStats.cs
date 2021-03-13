using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats
{
    [SerializeField] Stats stats = null;

    public int GetStatValue(StatType stat)
    {
        return stats.GetStatValue(stat);
    }

    public bool TryModifyStat(StatType stat, int value)
    {
        return stats.TryModifyStat(stat, value);
    }
}