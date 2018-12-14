﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject projectilePrefab;
    public Transform spawnPoint;


    private BoxCollider2D collider; 
    bool canShoot = true;
    bool playerDetected = false;
    float lastTimeShoot = 0;
    float timeShootCooldown = 1.3f;

    private Animator turretAnimator;

    private void Start()
    {
        turretAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(playerDetected)
        {
            if(canShoot)
            {
                turretAnimator.SetTrigger("Shoot");
                canShoot = false;
                lastTimeShoot = 0;
            }
            lastTimeShoot += Time.deltaTime;
            if(lastTimeShoot >= timeShootCooldown)
            {
                canShoot = true;
            }
        }
        
    }

    public void Shoot()
    {
        GameObject tmp = (GameObject)Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Throwable script = tmp.GetComponent<Throwable>();
        SoundManager.instance.ThrowKnife();

        if (script != null)
        {
            if (collider.offset.x < 0f)
            {
                script.SetDirection(Vector2.left);

            }
            else
            {
                script.SetDirection(Vector2.right);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;
            //Debug.Log("PlayerDetected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDetected = false;
        }
    }
}
