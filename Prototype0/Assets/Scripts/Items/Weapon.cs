using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon
{ 
    public int magical;
    public int physical;
    public WeaponType type;
    
    public Weapon(int physical, int magical, WeaponType type)
    {
        this.magical = magical;
        this.physical = physical;
        this.type = type;
    }
}

public enum WeaponType
{
    Sword,
    Axe,
    Trap,
    Fireball,
    Head,
    Knife
}