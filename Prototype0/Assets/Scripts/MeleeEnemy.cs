using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        attackRange = 1.1f;
        detectRange = 5f;
        movementSpeed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if(distance <= attackRange)
        {
            characterAnimator.SetTrigger("Attack");

        }
        else if (distance <= detectRange)
        {
            Flip(target.position.x - transform.position.x);
            Vector2 heading = target.position - transform.position;
            Vector2 direction = heading / distance;
            float deltaSpeed = movementSpeed * Time.deltaTime;
            transform.Translate(direction.x * deltaSpeed, 0.0f, 0.0f);
        }
    }

    protected override void Hit(Weapon weapon)
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
