using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controller : MonoBehaviour {
    //player movement files

    public float speed = 10f;
    public float maxSpeed = 15f;
    public float jumpForce = 3.2f;
    public float jump = 3.5f;
    public float jumpRatio = 0.4f;
    public static bool left = false;

    private Rigidbody2D body;
    private bool hasFlag = false;
    private bool isGrounded = true;
    private SpriteRenderer renderer; 
    
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horiz = 0f;
        float vert = 0f;

        var absvx = Mathf.Abs(body.velocity.x);
        if(isGrounded)
        {
            if(absvx < maxSpeed)
            {
                horiz = Input.GetAxis("Horizontal");
                if(horiz != 0)
                {
                    bool moveLeft = horiz > 0 ? false : true;
                    renderer.flipX = moveLeft;
                    if(left != moveLeft)
                    {
                        left = moveLeft;
                        body.velocity = Vector2.zero;

                    }
                    
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                Vector2 jumpVec = left ? new Vector2(-jump*jumpRatio, jump) : new Vector2(jump*jumpRatio, jump);
                body.AddForce(jumpVec * jumpForce, ForceMode2D.Impulse);
            }else
            {
                Vector2 movement = new Vector2(horiz * speed, vert);
                body.AddForce(movement);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter");
        GameObject colliding = collision.gameObject;
        if (colliding.CompareTag("Flag"))
        {
            Debug.Log("Bandiera");
            hasFlag = true;
            colliding.SetActive(false);
        }else if (colliding.CompareTag("Objective"))
        {
            if (hasFlag)
            {
                Debug.Log("You Have Won!");
            }
        }
        else if (colliding.CompareTag("Enemy"))
        {
            Debug.Log("You lost");
            //Restart();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")&&(!isGrounded))
        {
            isGrounded = true;
        }
    }

    public bool HasFlag()
    {
        return hasFlag;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
