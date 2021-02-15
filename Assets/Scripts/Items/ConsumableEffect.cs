using UnityEngine;

[System.Serializable]
public class ConsumableEffect
{
    public enum EffectType { Healing }

    [SerializeField] EffectType effect = EffectType.Healing;
    [SerializeField] int value = 10;

    public EffectType Effect => effect;
    public int Value => value;
}