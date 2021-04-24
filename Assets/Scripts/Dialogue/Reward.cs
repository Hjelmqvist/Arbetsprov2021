using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField] ItemInformation[] itemRewards = null;

    public bool TryGiveRewards(GameObject player, out string error)
    {
        error = "";
        if (itemRewards.Length > 0 && player.TryGetComponent(out PlayerInventory inventory))
        {
            if (!inventory.TryGiveItems(itemRewards))
            {
                error = "Not enough inventory space.";
                return false;
            }         
        }
        return true;
    }
}