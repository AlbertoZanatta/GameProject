using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenWaitingPlatform : MonoBehaviour, Damageable
{

    public SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;
    private MovingPlatform movingScript;
    private bool frozen = true;

    // Use this for initialization
    void Start ()
    {
        movingScript = GetComponent<MovingPlatform>();
        boxCollider = GetComponent<Collider2D>();

        if (movingScript != null)
        {
            movingScript.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !frozen)
        {
            Debug.Log("Player on unfrozen platform");
            if (movingScript != null)
            {
                movingScript.enabled = true;
            }
            boxCollider.enabled = false;
        }
    }

    public void Hit(Weapon weapon)
    {
        if (frozen)
        {
            spriteRenderer.color = Color.white;
            frozen = false;
        }
    }

    public void Reset()
    {
        spriteRenderer.color = Color.cyan;
        frozen = true;
        if(movingScript != null)
        {
            movingScript.enabled = false;
        }
        boxCollider.enabled = true;
    }
}
