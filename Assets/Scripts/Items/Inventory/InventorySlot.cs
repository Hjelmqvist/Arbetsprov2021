using UnityEngine;

public class InventorySlot
{
    [SerializeField] Item item = null;

    public Item Item => item;

    public void SetItem(Item item)
    {
        // item.Remove();
        this.item = item;
    }

    public void RemoveItem()
    {
        item = null;
    }
}
