using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public Vector2 startingVelocity = new Vector2(100, -100);
    public int bounceReps = 6;
    private Rigidbody2D body2D;

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        var startingVelocityX = -startingVelocity.x * transform.localScale.x;

        body2D.velocity = new Vector2(startingVelocityX, startingVelocity.y);
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.position.y > collision.gameObject.transform.position.y)
        {
            bounceReps--;
        }

        if(bounceReps <= 0)
        {
            //Debug.Log("Fireball vanished");
            Destroy(gameObject);
        }
    }
}
