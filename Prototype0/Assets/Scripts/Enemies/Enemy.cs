using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    public Transform target;
    public Item[] loot;

    [SerializeField] protected float detectRange;
    [SerializeField] protected float stopAttackRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float movementSpeed;

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
        if(horizontal_movement != 0)
        {
            bool flipSprite = (transform.localScale.x < 0 ? (horizontal_movement < 0) : (horizontal_movement > 0));
            if (flipSprite)
            {
                transform.localScale = (facingRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1));
                facingRight = !facingRight;
                
            }
        }    
    }
}
