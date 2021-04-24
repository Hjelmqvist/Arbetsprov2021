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

    public bool TryGiveItems(ItemInformation[] items) => inventory.TryGiveItems(items);
    public bool TryTakeItems(ItemInformation[] items) => inventory.TryTakeItems(items);
    public bool ContainsItems(ItemInformation[] items) => inventory.ContainsItems(items, out _);
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