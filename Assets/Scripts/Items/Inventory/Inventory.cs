using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] int slotCount = 32;

    [SerializeField] InventorySlot[] slots = null;

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }
}