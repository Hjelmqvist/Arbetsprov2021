using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats, ISavable
{
    //Stats that a character/class always starts with
    [SerializeField] CharacterBase characterBase = null;

    //Stats that are added from leveling up, using items etc.
    [SerializeField] Stats stats = null;

    public string CharacterName => characterBase.CharacterName;

    public delegate void StatsLoaded(PlayerStats stats);
    public static event StatsLoaded OnStatsLoaded;

    public delegate void StatChanged(StatType type, int value);
    public static event StatChanged OnStatChanged;

    void Start()
    {
        OnStatsLoaded?.Invoke(this);
    }

    public int GetStatValue(StatType stat)
    {
        return characterBase.GetStatValue(stat) + stats.GetStatValue(stat);
    }

    public bool TryModifyStat(StatType stat, int value)
    {
        if (stats.TryModifyStat(stat, value))
        {
            int newValue = GetStatValue(stat);
            OnStatChanged?.Invoke(stat, newValue);
            return true;
        }
        return false;
    }

    public object CaptureState()
    {
        return new PlayerStatsSave(characterBase.CharacterName, stats);
    }

    public void RestoreState(object info)
    {
        if (info is PlayerStatsSave save)
        {
            characterBase = CharacterBase.TryGetCharacterBase(save.character, out CharacterBase character) ? character : null;
            stats = save.stats;
            OnStatsLoaded?.Invoke(this);
        }
    }
}

[System.Serializable]
public class PlayerStatsSave
{
    public string character = "";
    public Stats stats = null;

    public PlayerStatsSave(string character, Stats stats)
    {
        this.character = character;
        this.stats = stats;
    }
}