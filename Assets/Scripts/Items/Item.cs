using UnityEngine;

/// <summary>
/// Unique values for an instance of an item.
/// 
/// </summary>
[System.Serializable]
public class Item
{
    [SerializeField] string itemName = "";

    ItemSO itemSO = null;
    InventorySlot currentInventorySlot = null;

    public string DisplayName => itemName;

    public ItemSO ItemInformation()
    {
        if (itemSO == null && ItemSO.TryGetItem(itemName, out ItemSO item))
            itemSO = item;
        return itemSO;
    }

    public Item(string name)
    {
        itemName = name;
    }

    public virtual void Use()
    {
        Debug.Log(itemName + " was used.");
    }
}
