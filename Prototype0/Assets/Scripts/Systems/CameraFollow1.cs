﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1 : MonoBehaviour {

    public GameObject player;
    public float scale = 20f;
    public float smoothSpeed = 10f;
    public float smoothVelocity = 0.3f;

    private Transform t;
    private SpriteRenderer s;
    private Vector3 rightOffset;
    private Vector3 leftOffset;
    private Vector3 velocity = Vector3.zero;
    private bool left;
    private float changeTime;
    private float leftBorder;
    private float halfWidth;


    private void Awake()
    {
        var cam = GetComponent<Camera>();
    }
    // Use this for initialization
    void Start () {
        t = player.transform;
        rightOffset =  transform.position - t.position;
        leftOffset = new Vector3(-rightOffset.x, rightOffset.y, rightOffset.z);
        left = true;
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
        leftBorder = transform.position.x - halfWidth;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desiredPosition = !GameController.instance.facingRight ? t.transform.position + leftOffset :t.transform.position + rightOffset;
        if(left != true)
        {
            left = true;
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
        if (transform.position.x  < 0)
        {
            //transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
	}
}
