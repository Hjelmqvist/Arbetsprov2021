using UnityEngine;

[System.Serializable]
public class Requirement
{
    [Tooltip("Items that has to be in the inventory, wont be taken")]
    [SerializeField] ItemLookup[] itemsInInventory = null;

    [Tooltip("Items that will be taken from inventory")]
    [SerializeField] ItemLookup[] itemsToTake = null;

    /// <summary>
    /// Returns true if all requirements can be fulfilled.
    /// </summary>
    public bool IsFulfilled(GameObject user)
    {
        //Look for items
        if (itemsInInventory.Length > 0 || itemsToTake.Length > 0)
        {
            if (!user.TryGetComponent(out PlayerInventory inventory) || 
                !inventory.ContainsItems(itemsInInventory) ||
                !inventory.ContainsItems(itemsToTake)) 
            {
                return false;
            }  
        }
        return true;
    }

    /// <summary>
    /// Take necessary items etc.
    /// </summary>
    public bool FulfillRequirements(GameObject user)
    {
        if (itemsToTake.Length > 0 && user.TryGetComponent(out PlayerInventory inventory))
        {
            if (!inventory.TryTakeItems(itemsToTake))
                return false;
        }
        return true;
    }
}