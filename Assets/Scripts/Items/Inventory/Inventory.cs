using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Inventory
{
    [SerializeField] InventorySlot[] slots = null;

    public int SlotCount => slots.Length;
    public InventorySlot[] Slots => slots;

    public bool TryAddItem(Item item)
    {
        if (item.TryGetInformation(out ItemSO info))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].HasItem)
                {
                    if (slots[i].Item.IsSameType(item) && info.IsStackable())
                    {
                        //Stack items
                        int overflow = slots[i].Item.ModifyAmount(item.Amount);
                        item.ModifyAmount(item.Amount - overflow);
                        if (item.Amount <= 0)
                            return true;
                    }
                }
                else
                {
                    slots[i].SetItem(item);
                    return true;
                }
            }
        }
        return false;
    }

    public bool TryGetItem(int slot, out Item item)
    {
        item = null;
        if (slot >= 0 && slot < slots.Length)
            item = slots[slot].Item;
        return item != null;
    }

    public bool TryTakeItems(ItemLookup[] lookups)
    {
        if (ContainsItems(lookups, out Dictionary<string, InventorySlot[]> slotsWithSpecifiedItems))
        {
            for (int i = 0; i < lookups.Length; i++)
            {
                int amountToTake = lookups[i].quantity;
                foreach (InventorySlot slot in slotsWithSpecifiedItems[lookups[i].item.ItemName])
                {
                    int taken = slot.Item.Amount;
                    amountToTake -= taken;

                    //Return x amount if we got more than enough.
                    if (amountToTake < 0)
                    {
                        slot.Item.ModifyAmount((-amountToTake) - slot.Item.Amount);
                        slot.SetItem(slot.Item);
                        break;
                    }    
                    else
                    {
                        slot.Item.ModifyAmount(-taken);
                        slot.SetItem(null);
                    }
                }
            }
            return true;
        }
        return false;
    }

    public bool ContainsItems(ItemLookup[] lookups, out Dictionary<string, InventorySlot[]> slotsWithSpecifiedItems)
    {
        Dictionary<string, InventorySlot[]> foundSlots = new Dictionary<string, InventorySlot[]>();

        foreach (ItemLookup lookup in lookups)
        {
            InventorySlot[] slotsWithItems = slots.Where(x => x.Item != null && x.Item.IsSameType(lookup.item)).ToArray();
            int totalAmount = slotsWithItems.Sum(x => x.Item.Amount);
            if (totalAmount < lookup.quantity)
            {
                slotsWithSpecifiedItems = null;
                return false;
            }
            foundSlots.Add(lookup.item.ItemName, slotsWithItems);
        }
        slotsWithSpecifiedItems = foundSlots;
        return true;
    }
}