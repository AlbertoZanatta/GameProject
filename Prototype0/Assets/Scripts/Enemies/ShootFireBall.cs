using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour {

    public float attackDelay = 2f;
    public GameObject projectile;
    public Transform target;
    public float attackRange = 10f;

    private float timeBetweenAttacks = 0f;
    private IEnumerator coroutine;
    private Animator charAnimator;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenAttacks = attackDelay + 1f;
        charAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
        if(projectile != null)
        {
            /*
            if(timeBetweenAttacks > attackDelay)
            {
                SpawnProjectile(transform.position);
                
                
                timeBetweenAttacks = 0;
            }

            timeBetweenAttacks += Time.deltaTime;
            */
            Shoot();
            

            //SpawnProjectile(transform.position);
            



        }

    }

    private void Shoot()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (charAnimator.GetBool("FireBallAttack") == true)
        {
            Debug.Log("fireball");
        }

        if (timeBetweenAttacks > attackDelay && distance <= attackRange)
        {
            charAnimator.SetTrigger("FireBallAttack");

            SpawnProjectile(transform.position);

            

            timeBetweenAttacks = 0;
        }

        timeBetweenAttacks += Time.deltaTime;
    }

    
    private void SpawnProjectile(Vector2 spawnPosition)
    {
        var spawn = Instantiate(projectile, spawnPosition, Quaternion.identity) as GameObject;
        spawn.transform.localScale = transform.localScale;
        
    }
   
}
