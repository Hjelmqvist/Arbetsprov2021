using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISavable
{
    [SerializeField] Inventory inventory = null;

    public delegate void InventoryLoaded(InventorySlot[] slots);
    public static event InventoryLoaded OnInventoryLoaded;

    void Start()
    {
        OnInventoryLoaded?.Invoke(inventory.Slots);
    }

    public bool TryGetItem(int slot, out Item item)
    {
        inventory.TryGetItem(slot, out Item foundItem);
        item = foundItem;
        return item != null;
    }

    public bool HasRoom(ItemLookup[] items)
    {
        return inventory.HasRoom(items);
    }

    public bool TryTakeItems(ItemLookup[] items)
    {
        return inventory.TryTakeItems(items);
    }

    public bool ContainsItems(ItemLookup[] items)
    {
        return inventory.ContainsItems(items, out _);
    }

    public object CaptureState()
    {
        return inventory;
    }

    public void RestoreState(object state)
    {
        inventory = (Inventory)state;
        OnInventoryLoaded?.Invoke(inventory.Slots);
    }
}