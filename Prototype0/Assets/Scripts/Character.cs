using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected int healthPoints;
    protected bool facingRight;

    //references to some components
    protected Collider2D characterCollider;
    protected Rigidbody2D characterRigidbody;
    protected Animator characterAnimator;
    protected SpriteRenderer characterRenderer;

	// Use this for initialization
	virtual protected void Start () {
        characterCollider = GetComponent<Collider2D>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    protected abstract void Move();
    protected abstract void Attack();
    protected abstract void Hit(Weapon weapon);
    protected abstract void Die();

    protected abstract void Flip(float horizontal_movement);
  

}
