using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISaveable
{
    [SerializeField] int slotCount = 32;

    [SerializeField] InventorySlot[] slots = null;
    [SerializeField] ItemSO[] allItems = null;

    void Awake()
    {
        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
            Item item = allItems[Random.Range(0, allItems.Length)].GetItemInstance();
            slots[i].SetItem(item);
        }
    }

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }

    public object CaptureState()
    {
        return slots;
    }

    public void RestoreState(object state)
    {
        slots = (InventorySlot[])state;
    }
}
