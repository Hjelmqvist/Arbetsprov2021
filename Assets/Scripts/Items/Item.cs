using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] string itemID = null;
    [SerializeField] string displayName = null;
    [SerializeField, TextArea(1, 3)] string description = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] int stackSize = 1;

    public string ItemID => itemID;
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public bool IsStackable(out int stackSize)
    {
        stackSize = this.stackSize;
        return stackSize > 1;
    }

    static Dictionary<string, Item> itemDatabase = null;

    public static bool TryGetItem<T>(string itemID, out T foundItem) where T : Item
    {
        if (itemDatabase == null)
        {
            itemDatabase = new Dictionary<string, Item>();
            Item[] allItems = Resources.LoadAll<Item>("");
            foreach (Item item in allItems)
            {
                if (!itemDatabase.ContainsKey(item.itemID))
                    itemDatabase[item.itemID] = item;
            }
        }
        foundItem = (T)itemDatabase[itemID];
        return foundItem != null;
    }

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrEmpty(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {
        
    }
}