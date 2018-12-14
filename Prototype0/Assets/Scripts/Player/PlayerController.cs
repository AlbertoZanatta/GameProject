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
            return instance;
        }
    }

    //Public components' references
    public OSCTransmitter transmitter; //For playing sounds
    public Inventory inventory; //Reference to the inventory system
    public Parry parryManager;
    public event System.EventHandler<CollectedCoinArgs> coinsCollected;
    public event System.EventHandler<PlayerDeadArgs> playerDead;
    public event System.EventHandler<PlayerFlagArgs> flagCollected;

    //Vertical movement speed
    public float jumpTakeOffSpeed = 10f;

    //Public properties (capital letters distinguish properties from normal variables)
    public bool DoAttack { get; set; }
    Coroutine coroutine;


    public bool StartJump { get; set; }

    public bool StopJump { get; set; }

    public bool Parry { get; set; }

    public bool OnGround { get; set; }

    public bool CanParry { get; set; }

    public bool FacingRight { get; set; }

    public bool OnIce { get; set; }



    //Cooldown on the player attack
    bool canAttack;
    float lastAttackTime;
    [SerializeField] float attackCooldown = 2f;


    //Detecting ground
    public LayerMask groundMask; //for detecting ground in function IsGrounded()
    private float distToGround;  //fordetecting ground too
    public Transform groundDetection;
    //private boolean to control player 'invincibility' moments
    private bool canBeHit = true;
    private void Awake()
    {
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        CanParry = true;

        distToGround = characterCollider.bounds.extents.y;
        inventory.itemUsed += Inventory_itemUsed;
        FacingRight = true;

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
            if(canAttack)
            {
                characterAnimator.SetTrigger("Attack");
                canAttack = false;
                lastAttackTime = 0;
            }
            
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

        lastAttackTime += Time.deltaTime;
        if (lastAttackTime >= attackCooldown)
        {
            canAttack = true;
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
        RaycastHit2D hit = Physics2D.Raycast(groundDetection.position, Vector3.down, 0.3f, groundMask); 
        Vector3 down = transform.TransformDirection(Vector3.down) * 0.3f;
        Debug.DrawRay(groundDetection.position, down, Color.green);

        return hit.collider != null;
    }

    void HandleMovement(float horizontal)
    {
        if(characterRigidbody.velocity.y < 0)
        {
            characterAnimator.SetBool("grounded", true);
        }
        if(!DoAttack)
        {
            if(!OnIce)
            {
                characterRigidbody.velocity = new Vector2(horizontal * movementSpeed, characterRigidbody.velocity.y);
            }
            else
            {
                float dir = facingRight ? -1 : 1;
                //Debug.Log("dir: " + dir + ", horizontal: " + horizontal);
                if(Mathf.Abs(characterRigidbody.velocity.x) <= movementSpeed || dir*horizontal >= 0)
                characterRigidbody.AddForce(new Vector2(horizontal * movementSpeed, characterRigidbody.velocity.y));
                
       
            }

            if (StartJump)
            {
                if (OnGround && !Parry)
                {
                    characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, jumpTakeOffSpeed);
                   
                }
                StartJump = false;
            }
            if(StopJump)
            {
                if(characterRigidbody.velocity.y > 0)
                {
                    characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, characterRigidbody.velocity.y * 0.5f);
                   

                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();

                }
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
                coroutine = StartCoroutine(HitFlashing(0.6f, 0.1f));
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
        if(playerDead != null)
        {
            playerDead(this, new PlayerDeadArgs());
        }
    }

    private void Inventory_itemUsed(object sender, InventoryEventArgs e)
    {
        e.item.OnUse(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            //Debug.Log("CoinCollected");
            if(coinsCollected != null)
            {
                coinsCollected(this, new CollectedCoinArgs());
            }
        }
        else
        {
            GameObject colliding = collision.gameObject;

            //Checking if it's a collectable item
            IInventoryItem item = colliding.GetComponent<IInventoryItem>();
            if (item != null)
            {
                inventory.AddItem(item);
                if (item.itemName.Equals("Flag"))
                {
                    if (flagCollected != null)
                    {
                        flagCollected(this, new PlayerFlagArgs(transform.position, SceneManager.GetActiveScene().name));
                    }
                }
                return;
            }
        }
    }

}

public class PlayerDeadArgs : System.EventArgs
{

}

public class PlayerFlagArgs : System.EventArgs
{
    public Vector3 catchPosition;
    public string levelName;
    public PlayerFlagArgs(Vector3 catchPosition, string levelName)
    {
        this.catchPosition = catchPosition;
        this.levelName = levelName;

    }
}
