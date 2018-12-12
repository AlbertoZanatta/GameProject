using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenWaitingPlatformRespawnController : RespawnController {

    FrozenWaitingPlatform frozenWaitingPlatform;
    MovingPlatform movingPlatform;

    private void Start()
    {
        frozenWaitingPlatform = gameObject.GetComponent<FrozenWaitingPlatform>();
        movingPlatform = gameObject.GetComponent<MovingPlatform>();
    }
    public override void OnRespawn()
    {
        base.OnRespawn();
        if(frozenWaitingPlatform != null)
        {
            frozenWaitingPlatform.Reset();
        }
        if (movingPlatform != null)
        {
            movingPlatform.Reset();
        }

    }
}
