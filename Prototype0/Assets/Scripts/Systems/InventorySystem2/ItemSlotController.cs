using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ItemSlotController : MonoBehaviour {
    public enum State
    {
        NEUTRAL,
        USE,
        DELETE
    }

    public const string NONE = "NONE";

    public Image frontImage;
    public Text text;
    public Image backImage;
    public ItemDragHandler dragHandler;

    private State state = State.NEUTRAL;
    private string itemName = NONE;
    private const float a = 0.48f;


    public void AddItem(IInventoryItem item)
    {
        text.text = 1.ToString();
        frontImage.enabled = true;
        frontImage.sprite = item.itemImage;
        this.itemName = item.itemName;
        dragHandler.Item = item;
        
    }

    public void ChangeQuantity(int quantity)
    {
        text.text = quantity.ToString();
    }

    public void RemoveItem()
    {
        text.text = "";
        frontImage.enabled = false;
        itemName = NONE;
        dragHandler.Item = null;
        ResetState();
    }

    public string ItemName
    {
        get { return itemName; }
    }

    public IInventoryItem Item
    {
        get { return dragHandler.Item; }
    }

    public void NextState()
    {
        if(itemName != NONE)
        {
            switch (state)
            {
                case State.NEUTRAL:
                    state = State.USE;
                    backImage.color = Color.green;
                    break;
                case State.USE:
                    state = State.DELETE;
                    backImage.color = Color.red;
                    
                    break;
                case State.DELETE:
                    state = State.NEUTRAL;
                    backImage.color = Color.white;
                    break;
            }

            backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, a);
        }
        
    }

    public void ResetState()
    {
        state = State.NEUTRAL;
        backImage.color = Color.white;
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, a);
    }

    public State Mode
    {
        get { return state; }
    }

    
	
}
