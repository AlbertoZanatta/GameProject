using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField] protected Health health;
    public float movementSpeed = 5f;

    protected bool facingRight = false;

    //references to some components
    public Collider2D characterCollider;
    public Rigidbody2D characterRigidbody;
    public Animator characterAnimator;
    //public property for accessing the animator
    public Animator CharacterAnimator { get{ return characterAnimator; } }
    public SpriteRenderer characterRenderer;

	// Use this for initialization
	virtual protected void Start () {
        characterCollider = GetComponent<Collider2D>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterRenderer = GetComponent<SpriteRenderer>();
        health.healthDepleted += Health_healthDepleted;
    }

    private void Health_healthDepleted(object sender, HealthEventArgs e)
    {
        Die();
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
    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();
    public abstract void Flip(float horizontal_movement);


    public abstract void Hit(Weapon weapon);

    public Health Health
    {
        get { return health; }
    }

    public virtual void Throw(GameObject throwablePrefab)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(throwablePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -45)));
            ThrowableKnife script = tmp.GetComponent<ThrowableKnife>();
            if (script != null)
            {
                script.Initialize(Vector2.right);
            }

        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(throwablePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -45)));
            ThrowableKnife script = tmp.GetComponent<ThrowableKnife>();
            if (script != null)
            {
                script.Initialize(Vector2.left);
            }
        }
    }


}
