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
        //Unsubrscribe from old item
        if (currentItem != null)
            currentItem.OnItemChanged -= CurrentItem_OnItemChanged;
        currentItem = item;

        //Subscribe to new item
        if (currentItem != null)
            currentItem.OnItemChanged += CurrentItem_OnItemChanged;
        OnItemChanged?.Invoke(item);
    }

    private void CurrentItem_OnItemChanged(Item item)
    {
        OnItemChanged?.Invoke(item);
    }
}