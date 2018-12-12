using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

    public float timeToDrop = 1f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Weapon weapon = new Weapon(1, 0, WeaponType.Trap);

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody.gravityScale = 0; //the stalactite remains firm in position
	}

    IEnumerator Shaking(float time, float freq)
    {
        float elapsedTime = 0f;
        bool isCyan = false;
        while(elapsedTime <= time)
        {
            if(!isCyan)
            {
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
            isCyan = !isCyan;
            yield return new WaitForSeconds(freq);
            elapsedTime += freq;

        }

        spriteRenderer.color = Color.white;
        rigidBody.gravityScale = 4f;
    }

    public void Drop()
    {
        StartCoroutine(Shaking(timeToDrop, timeToDrop / 10f));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
            rigidBody.gravityScale = 0;
            return;
        }

        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Hit(weapon);
        }
   
    }
}
