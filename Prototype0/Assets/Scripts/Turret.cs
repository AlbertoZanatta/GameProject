using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject projectilePrefab;
    public Transform spawnPoint;

    bool canShoot = true;
    bool playerDetected = false;
    float lastTimeShoot = 0;
    float timeShootCooldown = 2.5f;

    private Animator turretAnimator;

    private void Start()
    {
        turretAnimator = GetComponent<Animator>();

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
        GameObject tmp = (GameObject)Instantiate(projectilePrefab, spawnPoint.position, Quaternion.Euler(new Vector3(0, 0, -45)));
        ThrowableKnife script = tmp.GetComponent<ThrowableKnife>();
        if (script != null)
        {
            script.Initialize(Vector2.right);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;
            Debug.Log("PlayerDetected");
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
