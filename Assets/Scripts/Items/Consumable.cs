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
                ce.Use(user);
        }
    }
}