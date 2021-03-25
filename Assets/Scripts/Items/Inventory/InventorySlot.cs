using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public delegate void ItemChanged(Item item);
    public event ItemChanged OnItemChanged;

    [SerializeField] Item currentItem = null;
    bool hasItem = false;

    public Item Item => currentItem;

    public void SetItem(Item item)
    {
        // item.Remove();
        currentItem = item;
        hasItem = item != null;
    }

    public bool TryTakeItem(out Item item)
    {
        item = currentItem;
        return item != null;
    }
}