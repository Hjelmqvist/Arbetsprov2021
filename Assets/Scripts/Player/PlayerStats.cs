using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats
{
    [SerializeField] CharacterBase characterBase = null;

    public int GetStatValue(StatType stat)
    {
        return 0;
    }
}
