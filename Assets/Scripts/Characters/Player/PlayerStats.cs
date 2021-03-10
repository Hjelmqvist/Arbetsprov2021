using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats
{
    [SerializeField] CharacterBase characterBase = null;

    //TODO: Add instance stats

    public int GetStatValue(StatType stat)
    {
        return characterBase.GetStatValue(stat);
    }
}