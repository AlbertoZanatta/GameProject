using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : Character, Damageable {

    private IEnemyState currentState;
    [SerializeField] public StateMachine stateMachine;

    public GameObject Target { get; set; }
    [SerializeField] private float meleeRange = 1.2f;
    [SerializeField] private float throwRange = 3f;



    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }

            return false;
        }
    }



    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        SetStates();
        ChangeState(stateMachine.idleState); // now the enemy is in Idle State
    }

    protected abstract void SetStates();
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        currentState.Execute();
        LookAtTarget();
	}

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);

    }

    public override void Move()
    {
        Vector2 direction = GetDirection();
        characterRigidbody.velocity = new Vector2(GetDirection().x * movementSpeed, characterRigidbody.velocity.y);
        Debug.Log("velocity: " + characterRigidbody.velocity);
        characterAnimator.SetFloat("VelocityX", Mathf.Abs(characterRigidbody.velocity.x));
        characterAnimator.SetFloat("VelocityY", characterRigidbody.velocity.y);
    }

    public void MoveBack()
    {
        Vector2 direction = -GetDirection();
        characterRigidbody.velocity = new Vector2(direction.x * movementSpeed, characterRigidbody.velocity.y);
        characterAnimator.SetFloat("VelocityX", Mathf.Abs(characterRigidbody.velocity.x));
        characterAnimator.SetFloat("VelocityY", characterRigidbody.velocity.y);
    }

    public override void Flip(float horizontal_movement)
    {
        if (horizontal_movement != 0)
        {
            bool flipSprite = (transform.localScale.x < 0 ? (horizontal_movement < 0) : (horizontal_movement > 0));
            if (flipSprite)
            {
                transform.localScale = (facingRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1));
            }
        }
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void Attack()
    {
        
    }

    public override void Die()
    {
        Destroy(gameObject);  
    }

    public override void Hit(Weapon weapon)
    {
        int physicalDamage = weapon.physical;
        health.ChangeHealth(-physicalDamage);
        StartCoroutine(HitFlashing(1f, 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(collision);
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if(xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }
}
