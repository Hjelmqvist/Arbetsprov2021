using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references and information that should be shared for all items of the same type.
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string itemName = null;
    [SerializeField, TextArea(1, 3)] string description = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] int maxStack = 1;

    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
    public bool IsStackable()
    {
        return maxStack > 1;
    }
    public int MaxStack => maxStack;

    static Dictionary<string, ItemSO> itemDatabase = null;

    public static bool TryGetItem<T>(string itemID, out T foundItem) where T : ItemSO
    {
        if (itemDatabase == null)
        {
            itemDatabase = new Dictionary<string, ItemSO>();
            ItemSO[] allItems = Resources.LoadAll<ItemSO>("");
            foreach (ItemSO item in allItems)
            {
                if (!itemDatabase.ContainsKey(item.itemName))
                    itemDatabase[item.itemName] = item;
            }
        }
        foundItem = itemDatabase.TryGetValue(itemID, out ItemSO value) ? (T)value : null;
        return foundItem != null;
    }

    public virtual Item GetItemInstance()
    {
        return new Item(itemName);
    }
}