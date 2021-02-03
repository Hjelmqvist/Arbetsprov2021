using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    [SerializeField] int baseAttack = 42;
    [SerializeField] Enhancement enhancement = Enhancement.Atk;
    [SerializeField] float enhancementValue = 20;

    public enum Enhancement { CritRate, CritDmg, EleMastery, Recharge, Atk }
}