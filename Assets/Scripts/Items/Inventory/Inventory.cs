using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            foreach (ItemInformation info in items)
            {
                int amount = info.amount;
                foreach (InventorySlot slot in foundSlots[info.item.ItemName])
                {
                    if (slot.HasItem)
                    {
                        int amountToAdd = info.item.MaxStack - slot.Item.Amount;
                        if (amountToAdd > amount)
                            amountToAdd = amount;
                        amountToAdd = Mathf.Clamp(amountToAdd, 0, info.item.MaxStack);
                        slot.Item.ModifyAmount(amountToAdd);
                        amount -= amountToAdd;
                    }
                    else
                    {
                        int amountToAdd = Mathf.Clamp(amount, amount, info.item.MaxStack);
                        slot.SetItem(info.item.GetItemInstance(amountToAdd));
                        amount -= amountToAdd;
                    }

                    //Done
                    if (amount <= 0)
                        break;
                }
            }
            return true;
        }
        return false;
    }

    private bool HasRoom(ItemInformation[] items, out Dictionary<string, InventorySlot[]> foundSlots)
    {
        Dictionary<string, InventorySlot[]> specifiedItemSlots = new Dictionary<string, InventorySlot[]>();
        List<InventorySlot> takenSlots = new List<InventorySlot>();
        foreach (ItemInformation item in items)
        {
            List<InventorySlot> slotsForThisItem = new List<InventorySlot>();
            int amountNeeded = item.amount;

            //Stackable slots then empty slots
            List<InventorySlot> itemSlots = slots.Where(x => x.HasItem && x.Item.IsSameType(item.item) && !takenSlots.Contains(x)).ToList(); 
            itemSlots.AddRange(slots.Where(x => !x.HasItem && !takenSlots.Contains(x)));
            foreach (InventorySlot slot in itemSlots)
            {
                int availableSpace = slot.HasItem ? item.item.MaxStack - slot.Item.Amount : item.item.MaxStack;
                if (availableSpace > 0)
                {
                    takenSlots.Add(slot);
                    slotsForThisItem.Add(slot);
                    amountNeeded -= availableSpace;
                    if (amountNeeded <= 0)
                        break;
                }
            }
            if (amountNeeded <= 0)
            {
                specifiedItemSlots[item.item.ItemName] = slotsForThisItem.ToArray();
                continue;
            }
            foundSlots = null;
            return false;
        }
        foundSlots = specifiedItemSlots;
        return true;
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

    public bool ContainsItems(ItemInformation[] items, out Dictionary<string, InventorySlot[]> foundSlots)
    {
        Dictionary<string, InventorySlot[]> specifiedItemSlots = new Dictionary<string, InventorySlot[]>();

        foreach (ItemInformation item in items)
        {
            InventorySlot[] slotsWithItems = slots.Where(x => x.HasItem && x.Item.IsSameType(item.item)).ToArray();
            int totalAmount = slotsWithItems.Sum(x => x.Item.Amount);
            if (totalAmount < item.amount)
            {
                foundSlots = null;
                return false;
            }
            specifiedItemSlots.Add(item.item.ItemName, slotsWithItems);
        }
        foundSlots = specifiedItemSlots;
        return true;
    }
}