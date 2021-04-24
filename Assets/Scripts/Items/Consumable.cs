using UnityEngine;

[System.Serializable]
public class Consumable : Item
{
    public Consumable(string name, int amount) : base(name, amount) { }

    public override void Use(GameObject user)
    {
        if (TryGetInformation(out ConsumableSO information))
        {
            foreach (ConsumableEffect ce in information.Effects)
                ce.Use(user);
        }
    }
}