using UnityEngine;

/// <summary>
/// References and standard values for weapons
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponSO : ItemSO
{
    [SerializeField] int baseAttack = 42;
    [SerializeField] Enhancement secondary = Enhancement.Atk;
    [SerializeField] float secondaryValue = 20;

    public enum Enhancement { CritRate, CritDmg, EleMastery, Recharge, Atk }
}