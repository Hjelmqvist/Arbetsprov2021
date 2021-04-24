using UnityEngine;

[System.Serializable]
public class ConsumableEffect
{
    public enum EffectType { Strength, Intelligence }

    [SerializeField] EffectType effect = EffectType.Strength;
    [SerializeField] int value = 10;

    public void Use(GameObject user)
    {
        switch (effect)
        {
            case EffectType.Strength:
            case EffectType.Intelligence:
                if (user.TryGetComponent(out PlayerStats stats))
                    stats.TryModifyStat((StatType)effect, value);
                break;
            default:
                break;
        }
        
    }
}