using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class ConsumableSO : ItemSO
{
    [SerializeField] ConsumableEffect[] effects = null;

    public ConsumableEffect[] Effects => effects;

    public override Item GetItemInstance(int amount = 1)
    {
        return new Consumable(ItemName, amount);
    }
}