﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInventoryItem : MonoBehaviour {

    public ScriptableInventoryItem item;

    private void Start()
    {
        SpriteRenderer itemRenderer = GetComponent<SpriteRenderer>();
        itemRenderer.sprite = item.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collectable hit something!");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collectable got by Player!");
            //InventoryController inventory = collision.gameObject.GetComponent<InventoryController>();
           /* if(inventory)
            {
                Debug.Log("Adding to inventory!");
                if(inventory.AddItem(item))
                    EventManager.TriggerEvent("FeatherCollected");
            }*/
            Destroy(this.gameObject);
        }
    }
}
