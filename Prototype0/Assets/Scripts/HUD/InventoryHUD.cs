﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryHUD : MonoBehaviour{

    public Inventory inventory;
    public ItemSlotController[] itemSlots = new ItemSlotController[Inventory.SLOTS];

    private RectTransform rectTransform;
    public InventoryHUD instance;
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        inventory.itemAdded += InventoryScript_ItemAdded; //bind the item added event to be notified when an item is added to the inventory
        inventory.itemStacked += Inventory_itemStacked;
        inventory.itemRemoved += Inventory_itemRemoved;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Inventory_itemRemoved(object sender, InventoryEventArgs e)
    {
        foreach(ItemSlotController itemSlot in itemSlots)
        {
            if(itemSlot.ItemName.Equals(e.item.itemName))
            {
                itemSlot.RemoveItem();
                return;
            }
        }
    }

    private void Inventory_itemStacked(object sender, InventoryStackEventArgs e)
    {
        foreach (ItemSlotController itemSlot in itemSlots)
        {
            if (itemSlot.ItemName.Equals(e.itemName))
            { 
                itemSlot.ChangeQuantity(e.quantity);
                return;
            }
        }
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        foreach(ItemSlotController itemSlot in itemSlots)
        {
            if(itemSlot.ItemName.Equals(ItemSlotController.NONE))
            {
                itemSlot.AddItem(e.item);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        CheckInput();
	}

    private void CheckInput()
    {

        for (int i = 1; i <= itemSlots.Length; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {
                int selectedSlot = i - 1;
                inventory.UseItem(itemSlots[selectedSlot].Item);

            }
        }
        /*
        if(Input.GetButtonDown("Use"))
        {
            foreach(ItemSlotController itemSlot in itemSlots)
            {
                if(itemSlot.Mode != ItemSlotController.State.NEUTRAL)
                {
                    switch(itemSlot.Mode)
                    {
                        case ItemSlotController.State.USE:
                            inventory.UseItem(itemSlot.Item);
                            break;
                        case ItemSlotController.State.DELETE:
                            inventory.RemoveItem(itemSlot.Item);
                            break;
                    }
                }
            }
        }*/
    }
}
