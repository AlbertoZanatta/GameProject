using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenPlatformRespawnController : RespawnController{
    FrozenPlatform frozenPlatform;
    MovingPlatform movingPlatform;

    private void Start()
    {
        frozenPlatform = gameObject.GetComponent<FrozenPlatform>();
        movingPlatform = gameObject.GetComponent<MovingPlatform>();
    }
    public override void OnRespawn()
    {
        base.OnRespawn();
        if(frozenPlatform != null)
        {
            frozenPlatform.Reset();
        }
        if(movingPlatform != null)
        {
            movingPlatform.Reset();
        }
        
    }
}
