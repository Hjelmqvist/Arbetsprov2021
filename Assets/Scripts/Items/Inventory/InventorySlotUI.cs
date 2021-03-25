using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button = null;
    [SerializeField] TextMeshProUGUI amountText = null;
    [SerializeField] Sprite defaultImage = null;

    InventorySlot slot = null;
    Item item = null;
    ItemSO information = null;

    static InventorySlotUI fromSlot = null;
    static InventorySlotUI toSlot = null;

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
        item = newItem;
        information = null;

        if (newItem != null)
        {
            button.image.sprite = defaultImage;
            amountText.gameObject.SetActive(false);
        }
        else if (newItem.TryGetInformation(out ItemSO info))
        {
            information = info;
            button.image.sprite = info.Icon;
            amountText.text = newItem.Amount.ToString();
            amountText.gameObject.SetActive(info.IsStackable(out _));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Is dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toSlot = null;
    }
}