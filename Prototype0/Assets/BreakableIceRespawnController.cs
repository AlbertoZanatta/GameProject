using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIceRespawnController : RespawnController {

    BreakableIceBlock breakableIce;

    private void Start()
    {
        breakableIce = gameObject.GetComponent<BreakableIceBlock>();
    }

    public override void OnRespawn()
    {
        base.OnRespawn();
        if(breakableIce != null)
        {
            breakableIce.Reset();
        }
    }
}
