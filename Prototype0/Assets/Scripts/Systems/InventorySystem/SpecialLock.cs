using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialLock : MonoBehaviour
{
    [SerializeField] private string key;
    public string Key { get{ return key; } }

    [SerializeField] private TextMesh textMesh;
    [SerializeField] private string noItemText;
    [SerializeField] private string requestItemText;
    [SerializeField] private string agreedItemText;
    protected bool open = false;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!open)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (PlayerController.Instance.inventory.HasItem(key))
                {
                    textMesh.text = requestItemText;
                    Inventory.specialRequest = this;
                }
                else
                {
                    textMesh.text = noItemText;
                }
            }
        } 
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!open)
            {
            
                Inventory.specialRequest = null;
            }
            textMesh.text = "";

        }    
    }

    public  virtual void Agree(IInventoryItem item)
    {
        textMesh.text = agreedItemText;
        Inventory.specialRequest = null;
        Open();
    }

    protected virtual void Open()
    {
        open = true;
    }

}