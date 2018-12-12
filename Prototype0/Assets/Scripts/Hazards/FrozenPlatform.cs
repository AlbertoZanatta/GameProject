using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenPlatform : MonoBehaviour, Damageable {

    public SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;
    public MovingPlatform movingScript;

    public void Reset()
    {
        boxCollider.enabled = true;
        if(movingScript != null)
        {
            movingScript.enabled = false;
            spriteRenderer.color = Color.cyan;
        }
    }

    // Use this for initialization
    void Start ()
    {
        movingScript = GetComponent<MovingPlatform>();
        boxCollider = GetComponent<Collider2D>();

        if(movingScript != null)
        {
            movingScript.enabled = false;
        }
	}

    public void Hit(Weapon weapon)
    {
        if(movingScript != null)
        {
            spriteRenderer.color = Color.white;
            movingScript.enabled = true;
        }
        boxCollider.enabled = false;
    }


}
