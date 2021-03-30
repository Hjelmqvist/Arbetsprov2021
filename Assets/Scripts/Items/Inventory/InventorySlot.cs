using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public delegate void ItemChanged(Item item);
    public event ItemChanged OnItemChanged;

    [SerializeField] Item currentItem = null;
    public bool HasItem => currentItem != null && !string.IsNullOrEmpty(currentItem.ItemName);

    public Item Item => currentItem;

    public void SetItem(Item item)
    {
        currentItem = item;
        OnItemChanged?.Invoke(item);
    }
}