﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
    [SerializeField] int maxHealthPoints;
    [SerializeField] int healthPoints;
    public event EventHandler<HealthEventArgs> healthChange;
    public event EventHandler<HealthEventArgs> healthDepleted;

    public void ChangeHealth(int delta)
    {
        healthPoints += delta;
        if(healthChange != null)
        {
            healthChange(this, new HealthEventArgs(healthPoints));
        }
        if(healthPoints <= 0 && healthDepleted != null)
        {
            healthDepleted(this, new HealthEventArgs(healthPoints));
        }

    }

    public int MaxHealth
    {
        get { return maxHealthPoints; }
    }
}

public class HealthEventArgs : EventArgs
{
    public int healthPoints;

    public HealthEventArgs(int healthPoints)
    {
        this.healthPoints = healthPoints;
    }
}

