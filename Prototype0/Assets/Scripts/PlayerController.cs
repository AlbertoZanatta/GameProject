﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : Character
{
    
    public float speed = 5f; //velocità nello spostamento orizzontale
    public float jumpTakeOffSpeed = 10f;
    public float dodgeSpeed = 5f;
    public LayerMask groundMask;
    public float slowDown = 1f; //slow down coefficient due to parrying

    //private attributes
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;
    private Animator animator;
    private float distToGround;  // = collider.bounds.extents.y;

    //private boolean fields to control animations
    private bool grounded = true; //discriminate between walking/floating in air
    private bool facingRight = true;
    private bool parry = false;
    private bool attack = false;
    private bool start_jump = false;
    private bool stop_jump = false;
    private bool start_dodge = false;
    private bool stop_dodge = false;


    // Use this for initialization
    protected override void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        distToGround = boxCollider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        float horizontal_movement = Input.GetAxisRaw("Horizontal") * speed * slowDown;

        grounded = IsGrounded();
        animator.SetBool("grounded", grounded);

        HandleMovement(horizontal_movement);

        Flip(horizontal_movement);

        HandleAttacks();

        HandleDodge();

        ResetValues();
        
    }

    void HandleMovement(float horizontal_movement)
    {
        if(!IsAttacking() && !IsDodging())
        {
             body.velocity = new Vector2(horizontal_movement, body.velocity.y);
             

            float vertical_movement = body.velocity.y;

            if (grounded && start_jump)
            {
                vertical_movement = jumpTakeOffSpeed;
            }
            else if (stop_jump)
            {
                if (body.velocity.y > 0)
                {
                    vertical_movement = body.velocity.y * 0.3f;
                }
            }

            body.velocity = new Vector2(horizontal_movement, vertical_movement);

            animator.SetFloat("VelocityX", Mathf.Abs(horizontal_movement));
            animator.SetFloat("VelocityY", vertical_movement);
        }

    }

    void HandleAttacks()
    {
        if(attack)
        {
            animator.SetTrigger("Attack");
            body.velocity = Vector2.zero;
        }
    }

    void HandleDodge()
    {
        if(start_dodge)
        {
            Vector2 dodge_direction = facingRight ? Vector2.left: Vector2.right;
            body.AddForce(dodge_direction * dodgeSpeed, ForceMode2D.Impulse);
        }else if(stop_dodge)
        {
            if (Mathf.Abs(body.velocity.x) > 0)
            {
                body.velocity = new Vector2(body.velocity.x * 0.3f, body.velocity.y);
                Debug.Log("Forward: " + transform.forward+ ", epsilon: "+float.Epsilon);
            }
        }
    }

    void HandleInput()
    {
        if(Input.GetButtonDown("Attack"))
        {
            attack = true;

        }else if(Input.GetButtonDown("Jump"))
        {
            start_jump = true;

        }else if(Input.GetButtonUp("Jump"))
        {
            stop_jump = true;

        }else if(Input.GetButtonDown("Parry"))
        {
            parry = true;

        }else if(Input.GetButtonUp("Parry"))
        {
            parry = false;
        }else if(Input.GetButtonDown("Dodge"))
        {
            start_dodge = true;

        }else if(Input.GetButtonUp("Dodge"))
        {
            stop_dodge = true;
        }

    }

    protected override void Flip(float horizontal_movement)
    {
        bool flipSprite = (spriteRenderer.flipX ? (horizontal_movement > 0) : (horizontal_movement < 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingRight = !facingRight;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundMask);
        return hit.collider != null;
    }

    void HandleAttack()
    {
        if(grounded && attack && !IsAttacking())
        {
            animator.SetTrigger("Attack");
            body.velocity = Vector2.zero;
        }
    }

    bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("attack_animation");
    }

    bool IsDodging()
    {
        return (start_dodge || facingRight && body.velocity.x < -float.Epsilon || !facingRight && body.velocity.x > float.Epsilon);
    }

    void ResetValues()
    {
        attack = false;
        start_jump = false;
        stop_jump = false;
        start_dodge = false;
        stop_dodge = false;
    }

    void StopParry()
    {
        parry = false;
    }


    protected override void Attack()
    {
        //do somethingth
    }

    protected override void Hit(Weapon weapon)
    {
        int physicalDamage = weapon.physical;
        healthPoints -= physicalDamage;
        //characterAnimator.SetTrigger("Hit");
        if(healthPoints <= 0)
        {
            Die();
        }
    }

    protected override void Move()
    {
        //do something
    }

    protected override void Die()
    {
        //characterAnimator.SetBool("Dead", true);
        Debug.Log("Player is dead");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyHead"))
        {
            healthPoints -= 1;
            //characterAnimator.SetTrigger("Hit");
            Debug.Log("Player hits enemy head");
            Vector2 direction = facingRight ? Vector2.left : Vector2.right;
            body.AddForce(direction * 20);
        }
    }


    //Immunity after being hit


}
