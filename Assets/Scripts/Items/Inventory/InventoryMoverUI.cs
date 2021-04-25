using UnityEngine;
using UnityEngine.UI;

public class InventoryMoverUI : MonoBehaviour
{
    [SerializeField] Image image = null;

    private void OnEnable()
    {
        InventorySlotUI.OnSlotChange += InventorySlotUI_OnSlotChange;
    }

    private void OnDisable()
    {
        InventorySlotUI.OnSlotChange -= InventorySlotUI_OnSlotChange;
    }

    private void InventorySlotUI_OnSlotChange(InventorySlot slot, InventorySlotUI.SlotAction action)
    {
        switch (action)
        {
            case InventorySlotUI.SlotAction.BeginDrag:
                if (slot.HasItem && slot.Item.TryGetInformation(out ItemSO info))
                {
                    image.sprite = info.Icon;
                    image.enabled = true;
                }
                break;
            case InventorySlotUI.SlotAction.Drag:
                transform.position = Input.mousePosition;
                break;
            case InventorySlotUI.SlotAction.EndDrag:
                image.enabled = false;
                break;
            default:
                break;
        }
    }
}