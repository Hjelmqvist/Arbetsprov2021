using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    int currentHealth = 100;

    public void ModifyHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
    }
}
