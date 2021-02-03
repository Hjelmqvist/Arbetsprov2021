using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Item[] inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveManager.SaveXML(inventory, "Inventory");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (SaveManager.TryLoadXML("Inventory", out Item[] data))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Debug.Log(data[i]);
                }

                inventory = new Item[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    if (Item.TryGetItem(data[i].name, out Item item))
                    {
                        inventory[i] = item;
                    }

                }
            }
        }
    }
}
