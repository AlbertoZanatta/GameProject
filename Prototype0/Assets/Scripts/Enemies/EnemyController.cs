using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject player;
    public float attackDist = 5f;
    public float speed = 10;
    public CircleCollider2D headCollider;

	// Use this for initialization
	void Start ()
    {
        headCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= attackDist)
        {
            Vector2 heading = player.transform.position - transform.position;
            Vector2 direction = heading / distance;
            float deltaSpeed = speed * Time.deltaTime;
            transform.Translate(direction.x*deltaSpeed, 0.0f, 0.0f);
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&collision.otherCollider == headCollider)
        {
            this.gameObject.SetActive(false);
        }
    }
}
