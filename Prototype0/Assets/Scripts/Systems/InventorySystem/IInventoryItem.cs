using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class IInventoryItem: MonoBehaviour
{

    public string itemName;
    public Sprite itemImage;
    public bool enemyDropped = false;

    private void OnEnable()
    {
        PlayerController.Instance.playerDead += Instance_playerDead;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerDead -= Instance_playerDead;
    }

    private void Instance_playerDead(object sender, PlayerDeadArgs e)
    {
        if(enemyDropped)
        {
            Destroy(gameObject);
        }
    }

    public abstract void OnPickUp();
    public abstract void OnUse(Character character);
    public abstract void OnDrop();

     
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem item;

    public InventoryEventArgs(IInventoryItem item)
    {
        this.item = item;
    }
}