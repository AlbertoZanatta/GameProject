using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    public Transform target;
    public Item[] loot;

    protected float detectRange;
    protected float attackRange;
    protected float movementSpeed;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Flip(float horizontal_movement)
    {
            bool flipSprite = (characterRenderer.flipX ? (horizontal_movement > 0) : (horizontal_movement < 0));
            if (!flipSprite)
            {
                characterRenderer.flipX = !characterRenderer.flipX;
                facingRight = !facingRight;
            }
    }
}
