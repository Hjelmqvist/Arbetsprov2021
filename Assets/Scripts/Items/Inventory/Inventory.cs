using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] InventorySlot[] slots = null;

    public int SlotCount => slots.Length;
    public InventorySlot[] Slots => slots;

    public bool TryAddItem(Item item)
    {
        if (item.TryGetInformation(out ItemSO info))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].HasItem)
                {
                    if (slots[i].Item.ItemName == item.ItemName && info.IsStackable())
                    {
                        //Stack items
                        int overflow = slots[i].Item.ModifyAmount(item.Amount);
                        item.ModifyAmount(item.Amount - overflow);
                        if (item.Amount <= 0)
                            return true;
                    }
                }
                else
                {
                    slots[i].SetItem(item);
                    return true;
                }
            }
        }
        return false;
    }

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }
}