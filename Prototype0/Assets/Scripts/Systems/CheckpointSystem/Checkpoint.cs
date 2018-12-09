using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint: MonoBehaviour
{
    bool triggered;

    private void Awake()
    {
        triggered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggered)//First time the checkpoint is triggered
        {
            Debug.Log("CheckPoint triggered");
            PlayerRespawnController playerRespawnController = collision.gameObject.GetComponent<PlayerRespawnController>();
            if(playerRespawnController != null)
            {
                triggered = true;
                playerRespawnController.UpdateState();
                CheckpointManager.instance.SetCheckpoint(transform.position);
                
                
            }
        }
    }
}