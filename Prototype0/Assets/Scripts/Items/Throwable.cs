using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour{
    public Weapon weapon;
    public float speed;
    public float lifeTime = 3f;
    public float orientDegree = 0f; //Degrees to rotate the spriteobject so that it is in the RIGHT direction

    private float timePassed = 0; //Time passed since the object has being launched

    private Rigidbody2D rigidBody;
    private Vector2 direction;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {

        timePassed += Time.deltaTime;
        if (timePassed >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        if(direction == Vector2.right)
        {
            transform.Rotate(new Vector3(0, 0, orientDegree));
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.Rotate(new Vector3(0, 0, -orientDegree));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" || weapon.type==WeaponType.Knife)
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Hit(weapon);
                Destroy(gameObject);
            }
        }
    }
}
