using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnController : RespawnController{
    private int healthPoints;
    public List<ItemStack> copiedInventory;
    // Use this for initialization

    public override void OnRespawn()
    {
        //Debug.Log("PlayerRespawn!");
        base.OnRespawn();
        PlayerController.Instance.Health.Refill();
        if((GameController.instance.levelForward && PlayerController.Instance.transform.localScale.x < 0) || (!GameController.instance.levelForward && PlayerController.Instance.transform.localScale.x > 0))
        {
            Vector3 localScale = PlayerController.Instance.transform.localScale;
            PlayerController.Instance.transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            PlayerController.Instance.FacingRight = !PlayerController.Instance.FacingRight;
        }
        PlayerController.Instance.inventory.ResetInventory(copiedInventory);
        PlayerController.Instance.characterRigidbody.velocity = Vector2.zero;
    }

    public void UpdateState()
    {
        //Debug.Log("Update state!");
        initialPosition = PlayerController.Instance.transform.position;
        healthPoints = PlayerController.Instance.Health.CurrentHealth;
        copiedInventory = PlayerController.Instance.inventory.GetInventory(); 
    }
}
