using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon{

    public enum WeaponType{
        Sword,
        Axe, 
        Trap,
        Fireball,
        Head
    }

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
