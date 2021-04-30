using TMPro;
using UnityEngine;

public class PlayerInformationUI : MonoBehaviour
{
    [SerializeField] GameObject informationWindow = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI[] statTexts = null;
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
        PlayerStats.OnStatsLoaded += PlayerStats_OnStatsLoaded;
        PlayerStats.OnStatChanged += PlayerStats_OnStatChanged;
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryLoaded -= PlayerInventory_OnInventoryLoaded;
        PlayerStats.OnStatsLoaded -= PlayerStats_OnStatsLoaded;
        PlayerStats.OnStatChanged -= PlayerStats_OnStatChanged;
    }

    private void PlayerInventory_OnInventoryLoaded(InventorySlot[] slots)
    {
        for (int i = 0; i < inventorySlots.Length && i < slots.Length; i++)
            inventorySlots[i].SetInventorySlot(slots[i]);
    }

    private void PlayerStats_OnStatsLoaded(PlayerStats stats)
    {
        nameText.text = $"Name: {stats.CharacterName}";
        for (StatType type = StatType.Strength; type <= StatType.Intelligence; type++)
        {
            if ((int)type > statTexts.Length)
                break;
            statTexts[(int)type].text = $"{type}: {stats.GetStatValue(type)}";
        }
    }

    private void PlayerStats_OnStatChanged(StatType type, int value)
    {
        if ((int)type > statTexts.Length)
            return;
        statTexts[(int)type].text = $"{type}: {value}";
    }
}