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

    private List<ItemStack> mItems = new List<ItemStack>();
    
    private class ItemStack
    {
        IInventoryItem item;
        int quantity;

        public ItemStack(IInventoryItem item)
        {
            this.item = item;
            quantity = 1;
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

    public void UseItem(IInventoryItem item)
    {
        if(itemUsed != null)
        {
            itemUsed(this, new InventoryEventArgs(item));
            RemoveItem(item);
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
