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


    private void Awake()
    {
        var cam = GetComponent<Camera>();
       // cam.orthographicSize = (Screen.height / 2f) / scale;
    }
    // Use this for initialization
    void Start () {
        t = player.transform;
        rightOffset =  transform.position - t.position;
        leftOffset = new Vector3(-rightOffset.x, rightOffset.y, rightOffset.z);
        facingRight = GameController.instance.facingRight;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desiredPosition = GameController.instance.facingRight ? t.transform.position + rightOffset :t.transform.position + leftOffset;
        if(facingRight != GameController.instance.facingRight)
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
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
	}
}
