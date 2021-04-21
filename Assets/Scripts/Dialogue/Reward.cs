using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField] ItemLookup[] itemRewards = null;

    public bool CanGiveRewards(GameObject player)
    {
        if (itemRewards.Length > 0 && player.TryGetComponent(out PlayerInventory inventory))
        {
            if (!inventory.HasRoom(itemRewards))
                return false;
        }
        return true;
    }

    public void GiveRewards(GameObject player)
    {
        
    }
}