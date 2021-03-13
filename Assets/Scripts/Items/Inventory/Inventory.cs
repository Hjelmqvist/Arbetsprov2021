using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] int slotCount = 32;
    InventorySlot[] slots = null;

    public int SlotCount => slotCount;

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }
}