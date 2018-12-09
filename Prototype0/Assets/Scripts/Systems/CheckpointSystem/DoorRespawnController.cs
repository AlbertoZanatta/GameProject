using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRespawnController : RespawnController {

    public Door door;

    public override void OnRespawn()
    {
        base.OnRespawn();
        door.Close();
    }

}
