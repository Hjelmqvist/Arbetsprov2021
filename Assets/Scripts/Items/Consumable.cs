using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Consumable : Item
{
    public Consumable(string name) : base(name) { }

    public override void Use(GameObject user)
    {
        if (user.TryGetComponent(out PlayerStats stats) && TryGetInformation(out ConsumableSO information))
        {
            foreach (ConsumableEffect effect in information.Effects)
            {
                switch (effect.Effect)
                {
                    case ConsumableEffect.EffectType.Healing:
                        stats.ModifyHealth(effect.Value);
                        break;
                    default:
                        Debug.Log("Invalid ConsumableEffect.", information);
                        break;
                }
            }
        }
    }
}
