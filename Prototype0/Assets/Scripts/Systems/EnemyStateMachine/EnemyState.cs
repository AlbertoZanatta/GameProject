using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : Character, Damageable {

    
    public StateMachine stateMachine;
    public GameObject Target { get; set; }
    public Transform forwardEnvironmentDetection;
    public Transform backwardEnvironmentDetection;
    public LayerMask groundMask; //For detecting end of ground/impeding ground
    public LayerMask enemyMask; //For detecting other enemies
    public bool IsAttacking { get; set; }
    public bool ForceIdling { get; set; }

    private IEnemyState currentState;
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
        IsAttacking = false;
    }

    protected abstract void SetStates(); //Each enemy can implement its own set of states
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        currentState.Execute();
        
	}

    public void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
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

    public override void Move() //simply moves the enemy, either towards the player or in the patrolling direction
    {
        Vector2 direction = GetDirection();
        characterRigidbody.velocity = new Vector2(direction.x * movementSpeed, characterRigidbody.velocity.y);
        characterAnimator.SetFloat("VelocityX", Mathf.Abs(characterRigidbody.velocity.x));
        characterAnimator.SetFloat("VelocityY", characterRigidbody.velocity.y);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
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

    public override void Attack()
    {
        
    }

    public override void Die()
    {
       this.gameObject.SetActive(false);  
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

    //Function that returns true if the enemy is approaching the end of the current platform, false otherwise
    public bool OnPlatformEdge(bool checkForward)
    {
        Vector3 checkPoint = checkForward ? forwardEnvironmentDetection.position : backwardEnvironmentDetection.position;
        RaycastHit2D groundInfo = Physics2D.Raycast(checkPoint, Vector3.down, 0.5f, groundMask);
        Vector3 down = transform.TransformDirection(Vector3.down) * 0.5f;
        Debug.DrawRay(checkPoint, down, Color.green);

        if (groundInfo.collider == null)
        {
            return true;
        }

        return false;
    }

    //Function that returns true if the enemy is facing a ground block which prevents him to proceed further or another enemy.
    public bool IsGroundImpeded(bool checkForward)
    {
        Vector3 checkPoint = checkForward ? forwardEnvironmentDetection.position : backwardEnvironmentDetection.position;
        Vector2 rayDirection;
        if(checkForward)
        {
            rayDirection = facingRight ? Vector2.right : Vector2.left;
        }
        else
        {
            rayDirection = facingRight ? Vector2.left : Vector2.right;
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(checkPoint, rayDirection, 1f, groundMask);

        Vector3 direction = rayDirection * 2f;
        Debug.DrawRay(checkPoint, direction, Color.red);

        if (groundInfo.collider == null)
        {
            return false;
        }

        return true;
    }

    public bool IsEnemyImpeded(bool checkForward)
    {
        Vector3 checkPoint = checkForward ? forwardEnvironmentDetection.position : backwardEnvironmentDetection.position;
        Vector2 rayDirection;
        if (checkForward)
        {
            rayDirection = facingRight ? Vector2.right : Vector2.left;
        }
        else
        {
            rayDirection = facingRight ? Vector2.left : Vector2.right;
        }

        RaycastHit2D enemyInfo = Physics2D.Raycast(checkPoint, rayDirection, 3f, enemyMask);

        if (enemyInfo.collider == null)
        {
            return false;
        }

        return true;
    }

    public float targetDistance()
    {
        if (Target != null)
        {
            return Vector2.Distance(transform.position, Target.transform.position);
        }
        else
            return float.PositiveInfinity;
    }

    public void ResetBehaviour()
    {
        Target = null;
        ChangeState(stateMachine.idleState);  
    }




}
