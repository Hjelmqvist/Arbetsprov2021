using UnityEngine;
using UnityEngine.UI;

public class InventoryMoverUI : MonoBehaviour
{
    [SerializeField] Image image = null;

    private void OnEnable()
    {
        InventorySlotUI.OnBeginDragItem += InventorySlotUI_OnBeginDragItem;
        InventorySlotUI.OnDragItem += InventorySlotUI_OnDragItem;
        InventorySlotUI.OnDragItemEnd += InventorySlotUI_OnDragItemEnd;
    }

    private void OnDisable()
    {
        InventorySlotUI.OnBeginDragItem -= InventorySlotUI_OnBeginDragItem;
        InventorySlotUI.OnDragItem -= InventorySlotUI_OnDragItem;
        InventorySlotUI.OnDragItemEnd -= InventorySlotUI_OnDragItemEnd;
    }

    private void InventorySlotUI_OnBeginDragItem(Item item)
    {
        if (item != null && item.TryGetInformation(out ItemSO info))
        {
            image.sprite = info.Icon;
            image.enabled = true;
        }   
    }

    private void InventorySlotUI_OnDragItem(Vector2 pos)
    {
        transform.position = pos;
    }

    private void InventorySlotUI_OnDragItemEnd()
    {
        image.enabled = false;
    }
}