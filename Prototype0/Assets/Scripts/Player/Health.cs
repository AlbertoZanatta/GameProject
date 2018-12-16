using System;
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
        if (healthPoints > maxHealthPoints)
            healthPoints = maxHealthPoints;
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

    public int CurrentHealth
    {
        get { return healthPoints; }
    }

    public bool IsMax()
    {
        return CurrentHealth == maxHealthPoints;
    }

    public void Refill()
    {
        healthPoints = maxHealthPoints;
        if (healthChange != null)
        {
            healthChange(this, new HealthEventArgs(healthPoints));
        }
    }

    public void SetHealth(int healthPoints)
    {
        this.healthPoints = healthPoints;
        if (healthChange != null)
        {
            healthChange(this, new HealthEventArgs(this.healthPoints));
        }
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

