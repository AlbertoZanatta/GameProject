using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour, IInventoryItem {
    public Sprite image;
    public string itemName
    {
        get
        {
            return "Key_First_Level";
        }
    }

    public Sprite itemImage
    {
        get { return image; }
    }

    public void OnDrop()
    {
        
    }

    public void OnPickUp()
    {
        
    }

    public void OnUse(Character character)
    {
        
    }
   
}
