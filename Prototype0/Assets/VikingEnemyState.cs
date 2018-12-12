using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingEnemyState : MeleeEnemyState {
    public Transform groundDetection;

    protected override void Start()
    {
        base.Start();
        facingRight = true;
    }

    protected override void Update()
    {
        base.Update();
        OnGround = DetectGround();
    }

    private bool DetectGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector3.down, 0.5f, groundMask);
        Vector3 down = transform.TransformDirection(Vector3.down) * 0.5f;
        Debug.DrawRay(groundDetection.position, down, Color.yellow);

        if (groundInfo.collider == null)
        {
            return false;
        }

        return true;
    }
}
