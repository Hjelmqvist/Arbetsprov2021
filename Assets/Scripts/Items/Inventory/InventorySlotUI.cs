using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button = null;
    [SerializeField] TextMeshProUGUI amountText = null;
    [SerializeField] Image itemImage = null;

    InventorySlot slot = null;

    static InventorySlot fromSlot = null;
    static InventorySlot toSlot = null;

    public delegate void DragItemStart(Item item);
    public static event DragItemStart OnBeginDragItem;

    public delegate void DragItem(Vector2 pos);
    public static event DragItem OnDragItem;

    public delegate void DragItemEnd();
    public static event DragItemEnd OnDragItemEnd;

    public void SetInventorySlot(InventorySlot newSlot)
    {
        if (slot != null)
            slot.OnItemChanged -= ItemChanged;

        if (newSlot != null)
        {
            newSlot.OnItemChanged += ItemChanged;
            slot = newSlot;
            ItemChanged(slot.Item);
        }
    }

    private void ItemChanged(Item newItem)
    {
        if (newItem == null || string.IsNullOrEmpty(newItem.ItemName))
        {
            amountText.gameObject.SetActive(false);
            itemImage.enabled = false;
        }
        else if (newItem.TryGetInformation(out ItemSO info))
        {
            amountText.text = newItem.Amount.ToString();
            amountText.gameObject.SetActive(info.IsStackable());
            itemImage.enabled = true;
            itemImage.sprite = info.Icon;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.HasItem)
        {
            fromSlot = slot;
            OnBeginDragItem?.Invoke(slot.Item);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragItem?.Invoke(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragItemEnd?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (fromSlot.Item.ItemName == toSlot.Item.ItemName &&
                fromSlot.Item.TryGetInformation(out ItemSO info) && info.IsStackable())
        {
            //Stack items
            int overflow = toSlot.Item.ModifyAmount(fromSlot.Item.Amount);
            fromSlot.Item.ModifyAmount(overflow - fromSlot.Item.Amount);

            //To update UI elements
            fromSlot.SetItem(fromSlot.Item);
            toSlot.SetItem(toSlot.Item);
            if (fromSlot.Item.Amount <= 0)
                fromSlot.SetItem(null);
        }
        else
        {
            Item temp = toSlot.Item;
            toSlot.SetItem(fromSlot.Item);
            fromSlot.SetItem(temp);
        }
        OnDragItemEnd?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toSlot = slot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toSlot = null;
    }
}