using UnityEngine;

/// <summary>
/// Unique values for an instance of an item.
/// </summary>
[System.Serializable]
public class Item
{
    [SerializeField] string displayName = "";
    [SerializeField] int amount = 1;

    // Do not try to save the ScriptableObject to file
    [System.NonSerialized] ItemSO itemSO = null;

    public virtual string DisplayName => displayName;
    public int Amount => amount;

    public bool TryGetInformation<T>(out T information) where T : ItemSO
    {
        information = (T)itemSO;
        if (itemSO == null && ItemSO.TryGetItem(displayName, out T item))
        {
            itemSO = item;
            information = item;
        }
        return information != null;
    }

    public Item(string name)
    {
        displayName = name;
    }

    public virtual void Use(GameObject user)
    {
        Debug.Log(displayName + " was used.");
    }
}