using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using extOSC;

public class PlayerController : Character, Damageable
{
    //Singleton patter for acessing the player instance
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    //Public components' references
    public OSCTransmitter transmitter; //For playing sounds
    public Inventory inventory; //Reference to the inventory system
    public Parry parryManager;

    //Vertical movement speed
    public float jumpTakeOffSpeed = 10f;

    //Public properties (capital letters distinguish properties from normal variables)
    public bool DoAttack { get; set; }

    public bool StartJump { get; set; }

    public bool StopJump { get; set; }

    public bool Parry { get; set; }

    public bool OnGround { get; set; }

    public bool CanParry { get; set; }

    //Detecting ground
    public LayerMask groundMask; //for detecting ground in function IsGrounded()
    private float distToGround;  //fordetecting ground too

    //private boolean to control player 'invincibility' moments
    private bool canBeHit = true;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        CanParry = true;

        distToGround = characterCollider.bounds.extents.y;
        inventory.itemUsed += Inventory_itemUsed;
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
            characterAnimator.SetTrigger("Attack");
        }
        else if (Input.GetButtonDown("Jump"))
        {
            StartJump = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            StopJump = true;
        }
        else if (Input.GetButtonDown("Parry"))
        {
            if(CanParry)
            {
                characterAnimator.SetBool("Parry", true);
            }
           
        }
        else if (Input.GetButtonUp("Parry"))
        {
            characterAnimator.SetBool("Parry", false);
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        OnGround = IsGrounded();
        CharacterAnimator.SetBool("grounded", OnGround);
        HandleMovement(horizontal);
        Flip(horizontal);
        //HandleLayers 
    }

    public override void Move()
    {

    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundMask);
        return hit.collider != null;
    }

    void HandleMovement(float horizontal)
    {
        if(characterRigidbody.velocity.y < 0)
        {
            characterAnimator.SetBool("land", true);
        }
        if(!DoAttack)
        {
            characterRigidbody.velocity = new Vector2(horizontal * movementSpeed, characterRigidbody.velocity.y);

            if (StartJump && OnGround && !Parry)
            {
                characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, jumpTakeOffSpeed);
                StartJump = false;
            }

            if(StopJump && characterRigidbody.velocity.y > 0)
            {
                characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, characterRigidbody.velocity.y * 0.3f);
                StopJump = false;
            }
        }

        characterAnimator.SetFloat("VelocityX", Mathf.Abs(characterRigidbody.velocity.x));
        characterAnimator.SetFloat("VelocityY", characterRigidbody.velocity.y);
    }

    public override void Flip(float horizontal_movement)
    {
        bool flipSprite = (transform.localScale.x < 0 ? (horizontal_movement > 0) : (horizontal_movement < 0));
        if (flipSprite)
        {
            transform.localScale = (facingRight ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1));
            facingRight = !facingRight;
            GameController.instance.facingRight = facingRight;
        }
    }

    public override void Hit(Weapon weapon)
    {
        if(canBeHit)
        {
            int damage = weapon.physical + weapon.magical; //possibly a more complex function

            if(damage > 0)
            {
                health.ChangeHealth(-damage);
                StartCoroutine(HitFlashing(0.6f, 0.1f));
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

    public override void Die()
    {
        
    }

    private void Inventory_itemUsed(object sender, InventoryEventArgs e)
    {
        e.item.OnUse(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliding = collision.gameObject;

        //Checking if it's a collectable item
        IInventoryItem item = colliding.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
            return;
        }

    }
}
