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

    public bool TryAddItem(Item item)
    {
        return inventory.TryAddItem(item);
    }

    public bool TryGetItem(int slot, out Item item)
    {
        inventory.TryGetItem(slot, out Item foundItem);
        item = foundItem;
        return item != null;
    }

    public bool TryTakeItems(ItemLookup[] lookups)
    {
        return inventory.TryTakeItems(lookups);
    }

    public bool ContainsItems(ItemLookup[] lookups)
    {
        return inventory.ContainsItems(lookups, out _);
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