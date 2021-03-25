using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] InventorySlot[] slots = null;

    public int SlotCount => slots.Length;
    public InventorySlot[] Slots => slots;

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }
}