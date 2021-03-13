using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] Item item = null;

    public Item Item => item;

    public void SetItem(Item item)
    {
        // item.Remove();
        this.item = item;
    }

    public Item TakeItem()
    {
        if (item != null)
        {
            Item i = item;
            item = null;
            return i;
        }
        return null;
    }
}
