﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ThrowableKnife : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 3f;
    private float timePassed = 0;
    private Rigidbody2D rigidBody;
    private Vector2 direction;

    Weapon weapon = new Weapon(1, 0, Weapon.WeaponType.Knife);


	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }
    // Update is called once per frame
    void Update ()
    {

        timePassed += Time.deltaTime;
        if(timePassed >= lifeTime)
        {
            Destroy(gameObject);
        }
	}

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if(damageable != null)
            {
                damageable.Hit(weapon);
            }
        }
    }
}
