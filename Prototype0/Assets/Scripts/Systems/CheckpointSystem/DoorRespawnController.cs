using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRespawnController : RespawnController {
    Door door;
    private void Start()
    {
        door = gameObject.GetComponent<Door>();
    }

    public override void OnRespawn()
    {
        base.OnRespawn();
        if(door != null)
        {
            door.Close();
        }    
    }

}
