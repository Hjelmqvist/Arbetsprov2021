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
    public bool CanFulfillRequirements(GameObject player)
    {
        //Look for items
        if (itemsInInventory.Length > 0 || itemsToTake.Length > 0)
        {
            if (!player.TryGetComponent(out PlayerInventory inventory) || 
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
    public void FulfillRequirements(GameObject player)
    {
        if (itemsToTake.Length > 0 && player.TryGetComponent(out PlayerInventory inventory))
            inventory.TryTakeItems(itemsToTake);
    }
}