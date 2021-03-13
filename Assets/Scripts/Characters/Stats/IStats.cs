public interface IStats
{
    int GetStatValue(StatType stat);
    bool TryModifyStat(StatType stat, int value);
}