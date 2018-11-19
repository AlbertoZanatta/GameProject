using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float scale = 20f;
    public float smoothSpeed = 10f;
    public float smoothVelocity = 0.3f;

    private Transform t;
    private Vector3 rightOffset;
    private Vector3 leftOffset;

    private Vector3 velocity = Vector3.zero;
    private bool facingRight= true;
    private float changeTime;
    private bool smooth = false;

    //Set this bool to true when the player is jumping so that the camera doesn't continuously follow him
    public bool FollowPlayer { get; set; }
    public static CameraFollow instance;
    void Awake()
    {
        //Check if instance already exists
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

    }
    // Use this for initialization
    void Start () {
        t = player.transform;
        rightOffset =  transform.position - t.position;
        transform.position = t.transform.position + rightOffset;
        //rightOffset = new Vector3(rightOffset.x, 0, rightOffset.z);
        leftOffset = new Vector3(-rightOffset.x, rightOffset.y, rightOffset.z);
        //leftOffset = new Vector3(leftOffset.x, 0, leftOffset.z);
        facingRight = GameController.instance.facingRight;
        Debug.Log("Right offset: " + rightOffset);
        Debug.Log("Left offset: " + leftOffset);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //Camera follows up the player when he is about to exit the field of view
        Vector3 desiredPosition = GameController.instance.facingRight ? t.transform.position + rightOffset :t.transform.position + leftOffset;
        Vector3 currentOffset = transform.position - t.position;
        
        if(currentOffset.y <= rightOffset.y)
        {
            desiredPosition = new Vector3(desiredPosition.x, transform.position.y, desiredPosition.z);
        }
        else 
        {
            desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
        }
        
        if (facingRight != GameController.instance.facingRight)
        {
            facingRight = GameController.instance.facingRight;
            changeTime = Time.time;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothVelocity);
        }
        else if (Time.time - changeTime <= smoothVelocity)
        {
            
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothVelocity - (Time.time - changeTime));
        }
        else
        {
            transform.position = desiredPosition;
        }
        if (transform.position.x < -5)
        {
            //transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
	}
}
