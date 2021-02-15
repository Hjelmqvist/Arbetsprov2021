using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references and information that should be shared for all items of the same type.
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string displayName = null;
    [SerializeField, TextArea(1, 3)] string description = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] int stackSize = 1;

    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public bool IsStackable(out int stackSize)
    {
        stackSize = this.stackSize;
        return stackSize > 1;
    }

    static Dictionary<string, ItemSO> itemDatabase = null;

    public static bool TryGetItem(string itemID, out ItemSO foundItem)
    {
        if (itemDatabase == null)
        {
            itemDatabase = new Dictionary<string, ItemSO>();
            ItemSO[] allItems = Resources.LoadAll<ItemSO>("");
            foreach (ItemSO item in allItems)
            {
                if (!itemDatabase.ContainsKey(item.displayName))
                    itemDatabase[item.displayName] = item;
            }
        }
        foundItem = itemDatabase[itemID];
        return foundItem != null;
    }
}