using UnityEngine;

/// <summary>
/// Unique values for an instance of an item.
/// </summary>
[System.Serializable]
public class Item
{
    [SerializeField] string itemName = "";
    [SerializeField] int amount = 1;

    // Do not try to save the ScriptableObject to file
    [System.NonSerialized] ItemSO itemSO = null;

    public virtual string ItemName => itemName;
    public int Amount => amount;

    public delegate void ItemChanged(Item item);
    public event ItemChanged OnItemChanged;

    public Item(string name, int amount)
    {
        itemName = name;
        this.amount = amount;
    }

    /// <summary>
    /// Returns the overflowing amount.
    /// </summary>
    public int ModifyAmount(int amount)
    {
        if (TryGetInformation(out ItemSO info) && info.IsStackable)
        {
            this.amount += amount;
            int overflow = this.amount - info.MaxStack;
            if (overflow < 0)
                overflow = 0;
            this.amount -= overflow;
            OnItemChanged?.Invoke(this);
            return overflow;
        }
        Debug.Log($"{itemName} is missing an SO");
        return amount;
    }

    public bool TryGetInformation<T>(out T information) where T : ItemSO
    {
        information = (T)itemSO;
        if (itemSO == null && ItemSO.TryGetItem(itemName, out T item))
        {
            itemSO = item;
            information = item;
        }
        return information != null;
    }

    public virtual void Use(GameObject user)
    {
        Debug.Log(itemName + " was used.");
    }

    public bool IsSameType(Item other)
    {
        return other != null && itemName == other.itemName;
    }

    public bool IsSameType(ItemSO other)
    {
        return other != null && ItemName == other.ItemName;
    }
}