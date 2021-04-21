using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField] ItemInformation[] itemRewards = null;

    public bool CanGiveRewards(GameObject player, out string errorMessage)
    {     
        errorMessage = "";
        if (itemRewards.Length > 0 && player.TryGetComponent(out PlayerInventory inventory))
        {
            if (!inventory.HasRoom(itemRewards))
            {
                errorMessage = "Not enough inventory space.";
                return false;
            }         
        }
        return true;
    }

    public void GiveRewards(GameObject player)
    {
        //if (itemRewards.Length > 0 && player.TryGetComponent(out PlayerInventory inventory))
        //    inventory.GiveItems();
    }
}