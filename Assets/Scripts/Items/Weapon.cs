using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    public Weapon(string name, int amount) : base(name, amount) { }
}
