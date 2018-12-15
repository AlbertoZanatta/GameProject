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
    public bool isSpecial;

    private Collider2D collider;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collider.isTrigger = true;
            rigidbody.gravityScale = 0;
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