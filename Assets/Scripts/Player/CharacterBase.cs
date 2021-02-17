using System.Collections.Generic;
using UnityEngine;

public enum StatType { Strength, Intelligence, Dexterity }

/// <summary>
/// Base stats for a character/class
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public partial class CharacterBase : ScriptableObject
{
    [SerializeField] string characterName = "Bob";
    [SerializeField, Range(0, 100)] int strength = 50;
    [SerializeField, Range(0, 100)] int intelligence = 50;

    Dictionary<StatType, int> stats = null;

    public string CharacterName => characterName;

    /// <summary>
    /// Returns the value of a specific stat.
    /// If the stat does not exist it should return a 0.
    /// </summary>
    public int TryGetStatValue(StatType stat)
    {
        if (stats == null)
        {
            stats = new Dictionary<StatType, int>()
            {
                { StatType.Strength, strength },
                { StatType.Intelligence, intelligence }
            };
        }
        return stats[stat];
    }
}