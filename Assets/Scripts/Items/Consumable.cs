using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Consumable : Item
{
    public Consumable(string name) : base(name) { }

    public override void Use(GameObject user)
    {
        if (TryGetInformation(out ConsumableSO information))
        {
            foreach (ConsumableEffect ce in information.Effects)
            {
                switch (ce.Effect)
                {
                    case ConsumableEffect.EffectType.Strength:
                    case ConsumableEffect.EffectType.Intelligence:
                        //if (user.TryGetComponent(out PlayerStats stats))
                        //{
                        //    stats.ModifyStat(ce.Effect, ce.Value);
                        //}
                        break;
                    default:
                        Debug.Log("Invalid ConsumableEffect.", information);
                        break;
                }
            }
        }
    }
}