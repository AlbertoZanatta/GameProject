using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour, IInventoryItem {
    public Sprite sprite;

    public string itemName
    {
        get
        {
            return "Flag";
        }
    }

    public Sprite itemImage
    {
        get { return sprite; }
    }

    public void OnDrop()
    {
       
    }

    public void OnPickUp()
    {
        Destroy(gameObject);
    }

    public void OnUse(Character character)
    {
       
    }
}
