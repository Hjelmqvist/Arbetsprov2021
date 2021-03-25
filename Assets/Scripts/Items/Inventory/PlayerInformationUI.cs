using UnityEngine;

public class PlayerInformationUI : MonoBehaviour
{
    [SerializeField] GameObject informationWindow = null;
    [SerializeField] InventorySlotUI[] inventorySlots = null;
    [SerializeField] string inventoryButton = "Inventory";

    void Update()
    {
        if (Input.GetButtonDown(inventoryButton))
            informationWindow.SetActive(!informationWindow.activeSelf);
    }

    private void OnEnable()
    {
      
        PlayerInventory.OnInventoryLoaded += PlayerInventory_OnInventoryLoaded;
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryLoaded -= PlayerInventory_OnInventoryLoaded;
    }

    private void PlayerInventory_OnInventoryLoaded(InventorySlot[] slots)
    {
        for (int i = 0; i < inventorySlots.Length && i < slots.Length; i++)
            inventorySlots[i].SetInventorySlot(slots[i]);
    }
}