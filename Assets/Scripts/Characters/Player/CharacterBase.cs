using UnityEngine;

public enum StatType { Strength, Intelligence }

/// <summary>
/// Base information for a type of character/class
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public partial class CharacterBase : ScriptableObject, IStats
{
    [SerializeField] string characterName = "Bob";
    [SerializeField] Stats stats = null;

    public string CharacterName => characterName;

    public int GetStatValue(StatType stat)
    {
        return stats.GetStatValue(stat);
    }
}