using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public const int SLOTS = 4;
    public event EventHandler<InventoryEventArgs> itemAdded;
    public event EventHandler<InventoryStackEventArgs> itemStacked;
    public event EventHandler<InventoryEventArgs> itemRemoved;
    public event EventHandler<InventoryEventArgs> itemUsed;

    public static SpecialLock specialRequest = null;
    public List<ItemStack> mItems = new List<ItemStack>();
    
    

    public void AddItem(IInventoryItem item)
    {
        ItemStack found = FindItem(item);
        if(found != null)
        {
            found.Increment();

            if (itemStacked != null)
            {
                itemStacked(this, new InventoryStackEventArgs(item.itemName, found.Quantity));
            }

            item.OnPickUp();
        }
        else if(mItems.Count < SLOTS)
        {
            mItems.Add(new ItemStack(item)); //Add the item to the inventory
            item.OnPickUp();  //OnPickUp() method is called
            if(itemAdded != null)
            {
                itemAdded(this, new InventoryEventArgs(item)); //ItemAdded event raised and all the subscribers for this event are notified
            }

        }
    }

    internal void ResetInventory(List<ItemStack> copiedInventory)
    {
        foreach(ItemStack itemStack in mItems)
        {
            if(itemRemoved != null)
            {
                itemRemoved(this, new InventoryEventArgs(itemStack.Item));
            }
        }

        mItems.Clear();
        foreach (ItemStack item in copiedInventory)
        {
            mItems.Add(new ItemStack(item.Item, item.Quantity));
        }

        foreach (ItemStack itemStack in mItems)
        {
            if (itemAdded != null)
            {
                itemAdded(this, new InventoryEventArgs(itemStack.Item)); 
            }
        }

        foreach (ItemStack itemStack in mItems)
        {
            if (itemStacked != null)
            {
                itemStacked(this, new InventoryStackEventArgs(itemStack.Item.itemName, itemStack.Quantity));
            }
        }

    }

    public void UseItem(IInventoryItem item)
    {
        if(!item.isSpecial)
        {
            RemoveItem(item);
            if(itemUsed != null)
            {
                itemUsed(this, new InventoryEventArgs(item));
            }
                
        }
        else if(specialRequest != null)
        {
           
            if(specialRequest != null) //There's the possibility to interact and use a special item
            {

                if (specialRequest.Key.Equals(item.itemName))
                {
                    specialRequest.Agree(item);
                    RemoveItem(item);
                    if (itemUsed != null)
                    {
                        itemUsed(this, new InventoryEventArgs(item));
                    }
                }
                        
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        ItemStack found = FindItem(item);
        if (found != null)
        {
            int quantity = found.Decrement();

            if (itemStacked != null)
            {
                itemStacked(this, new InventoryStackEventArgs(item.itemName, found.Quantity));
            } 

            if(quantity <= 0)
            {
                mItems.Remove(found);
                if(itemRemoved != null)
                {
                    itemRemoved(this, new InventoryEventArgs(item));
                }
            }

        }
        else if (mItems.Count < SLOTS)
        {
            mItems.Add(new ItemStack(item)); //Add the item to the inventory
            item.OnPickUp();  //OnPickUp() method is called
            if (itemAdded != null)
            {
                itemAdded(this, new InventoryEventArgs(item)); //ItemAdded event raised and all the subscribers for this event are notified
            }

        }

    }

    private ItemStack FindItem(IInventoryItem item)
    {
        foreach(ItemStack iStack in mItems)
        {
            if (iStack.Item.itemName.Equals(item.itemName))
                return iStack;      
        }
        return null;
    }

    public bool HasItem(string item)
    {
        foreach (ItemStack iStack in mItems)
        {
            if (iStack.Item.itemName.Equals(item))
                return true;
        }
        return false;
    }

    public List<ItemStack> GetInventory()
    {
        List<ItemStack> copiedInventory = new List<ItemStack>();
        foreach(ItemStack item in mItems)
        {
            copiedInventory.Add(new ItemStack(item.Item, item.Quantity));
        }
        return copiedInventory;
    }
}

public class InventoryStackEventArgs : EventArgs
{
    public string itemName;
    public int quantity;

    public InventoryStackEventArgs(string itemName, int quantity)
    {
        this.itemName = itemName;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class ItemStack
{
    IInventoryItem item;
    int quantity;

    public ItemStack(IInventoryItem item)
    {
        this.item = item;
        quantity = 1;
    }

    public ItemStack(IInventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public int Increment()
    {
        quantity++;
        return quantity;
    }

    public int Decrement()
    {
        quantity--;
        return quantity;

    }

    public IInventoryItem Item
    {
        get { return item; }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }
    }
}
