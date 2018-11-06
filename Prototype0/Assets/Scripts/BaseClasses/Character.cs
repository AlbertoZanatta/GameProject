using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected int healthPoints;
    protected bool facingRight = false;

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

    protected virtual IEnumerator HitFlashing(float time, float frequency)
    {
        float elapsedTime = 0;
        bool isRed = false;

        while (elapsedTime <= time)
        {
            characterRenderer.color = isRed ? Color.white : Color.red;
            isRed = !isRed;
            yield return new WaitForSeconds(frequency);
            elapsedTime += frequency;
        }

        characterRenderer.color = Color.white;
    }

    //Methods to implement
    protected abstract void Move();
    protected abstract void Attack();
    protected abstract void Die();
    protected abstract void Flip(float horizontal_movement);



    public abstract void Hit(Weapon weapon);


}
