﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, Damageable {

    public Damageable holder;
    [SerializeField] int magicDefense;
    [SerializeField] int physicalDefense;
    [SerializeField] int healthPoints;

    public Shield(int magicDefense, int physicalDefense, int healthPoints)
    {
        this.magicDefense = magicDefense;
        this.physicalDefense = physicalDefense;
        this.healthPoints = healthPoints;
    }

    public void Hit(Weapon weapon)
    {
        int damage = physicalDefense - weapon.physical;
        if(damage > 0)
        {
            weapon.physical = damage;
            holder.Hit(weapon);
        }
    }


}
