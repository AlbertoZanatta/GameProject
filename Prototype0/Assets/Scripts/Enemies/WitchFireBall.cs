using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFireBall : MonoBehaviour {

    public Vector2 startingVelocity = new Vector2(100, -100);
    public int bounceReps = 6;
    private Rigidbody2D body2D;
    private Weapon weapon = new Weapon(0, 1, Weapon.WeaponType.Fireball);
  


    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        var startingVelocityX = startingVelocity.x * transform.localScale.x;

        body2D.velocity = new Vector2(startingVelocityX, startingVelocity.y);
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject hit = collision.gameObject;
        if(!hit.CompareTag("Enemy"))
        {
            Damageable hitScript = hit.GetComponent<Damageable>();
            if(hitScript != null)
            {
                hitScript.Hit(weapon);
            }
            Destroy(gameObject);

        }  
    }

    private IEnumerator Autodestroy()
    {
        yield return new WaitForSeconds(3f);
        if(gameObject)
        {
            Destroy(gameObject);
        }
    }


}
