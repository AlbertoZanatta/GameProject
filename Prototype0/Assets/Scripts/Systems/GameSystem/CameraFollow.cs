using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float scale = 20f;
    public float smoothSpeed = 10f;
    public float smoothVelocity = 0.3f;

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

        DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void Start () {
        ResetCamera();
        facingRight = PlayerController.Instance.FacingRight;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 desiredPosition = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
     
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
	}

    public void ResetCamera()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
    }
}
