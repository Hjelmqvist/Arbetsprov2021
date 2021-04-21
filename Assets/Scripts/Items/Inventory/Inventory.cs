using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class Inventory
{
    [SerializeField] InventorySlot[] slots = null;

    public int SlotCount => slots.Length;
    public InventorySlot[] Slots => slots;

    /* METHODS FOR GIVING ITEMS */ 

    public bool TryGiveItems(ItemInformation[] items)
    {
        if (HasRoom(items, out var foundSlots))
        {

        }
        return true;
    }

    public bool HasRoom(ItemInformation[] items, out Dictionary<string, InventorySlot[]> foundSlots)
    {
        //TODO: Check if inventory has enough space. Reverse ContainsItems pretty much
        throw new NotImplementedException();
    }

    /* METHODS FOR TAKING ITEMS */

    public bool TryTakeItems(ItemInformation[] lookups)
    {
        if (ContainsItems(lookups, out var foundSlots))
        {
            for (int i = 0; i < lookups.Length; i++)
            {
                int amountToTake = lookups[i].amount;
                foreach (InventorySlot slot in foundSlots[lookups[i].item.ItemName])
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

    public bool ContainsItems(ItemInformation[] lookups, out Dictionary<string, InventorySlot[]> foundSlots)
    {
        Dictionary<string, InventorySlot[]> specifiedItemSlots = new Dictionary<string, InventorySlot[]>();

        foreach (ItemInformation lookup in lookups)
        {
            InventorySlot[] slotsWithItems = slots.Where(x => x.Item != null && x.Item.IsSameType(lookup.item)).ToArray();
            int totalAmount = slotsWithItems.Sum(x => x.Item.Amount);
            if (totalAmount < lookup.amount)
            {
                foundSlots = null;
                return false;
            }
            specifiedItemSlots.Add(lookup.item.ItemName, slotsWithItems);
        }
        foundSlots = specifiedItemSlots;
        return true;
    }
}