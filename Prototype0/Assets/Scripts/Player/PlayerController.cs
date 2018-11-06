using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character, Damageable
{
    
    public float speed = 5f; //velocità nello spostamento orizzontale
    public float jumpTakeOffSpeed = 10f;
    public float dodgeSpeed = 5f;
    public LayerMask groundMask;
    public float slowDown = 1f; //slow down coefficient due to parrying

    //private attributes
    private float distToGround;  // = collider.bounds.extents.y;

    //Health system UI
    public Slider healthSlider;

    //private boolean fields to control animations
    private bool grounded = true; //discriminate between walking/floating in air
    private bool start_parry = false;
    private bool stop_parry = false;
    private bool attack = false;
    private bool start_jump = false;
    private bool stop_jump = false;
    private bool canBeHit = true;
    private bool canParry = true;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        distToGround = characterCollider.bounds.extents.y;
        healthPoints = 10;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            attack = true;

        }
        else if (Input.GetButtonDown("Jump"))
        {
            start_jump = true;

        }
        else if (Input.GetButtonUp("Jump"))
        {
            stop_jump = true;

        }
        else if (Input.GetButtonDown("Parry"))
        {
            start_parry = true;

        }
        else if (Input.GetButtonUp("Parry"))
        {
            stop_parry = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        float horizontal_movement = Input.GetAxisRaw("Horizontal") * speed * slowDown;

        grounded = IsGrounded();

        characterAnimator.SetBool("grounded", grounded);

        HandleMovement(horizontal_movement); //Handles horizontal movement and Jump

        Flip(horizontal_movement);

        HandleAttacks();

        HandleParry();

        ResetValues();
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundMask);
        return hit.collider != null;
    }

    void HandleMovement(float horizontal_movement)
    {
        if(!IsAttacking() && !IsParrying())
        {
             characterRigidbody.velocity = new Vector2(horizontal_movement, characterRigidbody.velocity.y);
             

            float vertical_movement = characterRigidbody.velocity.y;

            if (grounded && start_jump)
            {
                vertical_movement = jumpTakeOffSpeed;
            }
            else if (stop_jump)
            {
                if (characterRigidbody.velocity.y > 0)
                {
                    vertical_movement = characterRigidbody.velocity.y * 0.3f;
                }
            }

            characterRigidbody.velocity = new Vector2(horizontal_movement, vertical_movement);

            characterAnimator.SetFloat("VelocityX", Mathf.Abs(horizontal_movement));
            characterAnimator.SetFloat("VelocityY", vertical_movement);
        }

    }

    protected override void Flip(float horizontal_movement)
    {
        bool flipSprite = (transform.localScale.x < 0 ? (horizontal_movement > 0) : (horizontal_movement < 0));
        if (flipSprite)
        {
            transform.localScale = (facingRight ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1));
            facingRight = !facingRight;
            GameController.instance.facingRight = facingRight;
        }
    }

    void HandleAttacks()
    {
        if(grounded && attack && !IsAttacking())
        {
            Attack();   
        }
    }

    bool IsAttacking()
    {
        return characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack_animation");
    }

    protected override void Attack()
    {
        characterAnimator.SetTrigger("Attack");
        characterRigidbody.velocity = Vector2.zero;
        stop_parry = true;
    }

    private void HandleParry()
    {
        if(start_parry && canParry)
        {
            characterAnimator.SetBool("Parry", true);
            StartCoroutine("RechargeParry");
        }else if (stop_parry)
        {
            characterAnimator.SetBool("Parry", false);
        }

    }

    bool IsParrying()
    {
        return characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("parry_animation");
    }

    private IEnumerator RechargeParry()
    {
        canParry = false;
        yield return new WaitForSeconds(1.5f);
        canParry = true;
    }

    void ResetValues()
    {
        attack = false;
        start_jump = false;
        stop_jump = false;
        start_parry = false;
        stop_parry = false;
    }

    void StopParry()
    {
        start_parry = false;
    }

    public override void Hit(Weapon weapon)
    {
        if(canBeHit && !IsParrying())
        {
            int damage = weapon.physical + weapon.magical; //possibly a more complex function

            if(damage > 0)
            {
                healthPoints -= damage;
                healthSlider.value = healthPoints < 0 ? 0 : healthPoints;
                StartCoroutine(HitFlashing(0.6f, 0.1f));
                if (healthPoints <= 0)
                {
                    Die();
                }
            }  
        }  
    }

    protected override IEnumerator HitFlashing(float time, float frequency)
    {
        float elapsedTime = 0;
        bool isRed = false;
        canBeHit = false;

        while (elapsedTime <= time)
        {
            characterRenderer.color = isRed ? Color.white : Color.red;
            isRed = !isRed;
            yield return new WaitForSeconds(frequency);
            elapsedTime += frequency;
        }

        characterRenderer.color = Color.white;
        canBeHit = true;
    }

    protected override void Die()
    {
        //characterAnimator.SetBool("Dead", true);
        //Debug.Log("Player is dead");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyHead"))
        {
            healthPoints -= 1;
            //characterAnimator.SetTrigger("Hit");
            Debug.Log("Player hits enemy head");
            Vector2 direction = facingRight ? Vector2.left : Vector2.right;
            characterRigidbody.AddForce(direction * 20);
        }
    }

    //Immunity after being hit


}
