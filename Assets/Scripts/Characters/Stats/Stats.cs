﻿using System.Collections.Generic;
using UnityEngine;

public enum StatType { Strength, Intelligence }

[System.Serializable]
public class Stats : IStats
{
    [SerializeField, Range(0, 100)] int strength = 50;
    [SerializeField, Range(0, 100)] int intelligence = 50;

    Dictionary<StatType, int> stats = null;

    /// <summary>
    /// Returns the value of a specific stat.
    /// If specified stat does not exist return 0.
    /// </summary>
    public int GetStatValue(StatType stat)
    {
        InitializeStats();
        return stats.TryGetValue(stat, out int value) ? value : 0;
    }

    public bool TryModifyStat(StatType stat, int value)
    {
        InitializeStats();
        if (stats.TryGetValue(stat, out int value2))
        {
            value2 += value;
            stats[stat] = value2;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates the stats dictionary if its not made already
    /// </summary>
    private void InitializeStats()
    {
        if (stats == null)
        {
            //Setup here in order to use the variables
            stats = new Dictionary<StatType, int>()
            {
                { StatType.Strength, strength },
                { StatType.Intelligence, intelligence }
            };
        }
    }
}