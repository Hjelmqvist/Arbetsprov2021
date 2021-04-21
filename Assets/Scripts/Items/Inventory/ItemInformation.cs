//TODO: Think of a better name

/// <summary>
/// Container for x amount of y item.
/// Used for inventory search, requirements, rewards etc.
/// </summary>
[System.Serializable]
public class ItemInformation
{
    public ItemSO item;
    public int amount = 0;
}