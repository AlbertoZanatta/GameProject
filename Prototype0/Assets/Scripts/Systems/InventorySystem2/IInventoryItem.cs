using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{

	string itemName { get; }
    Sprite itemImage { get; }

    void OnPickUp();
    void OnUse(Character character);
    void OnDrop();
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem item;

    public InventoryEventArgs(IInventoryItem item)
    {
        this.item = item;
    }
}