using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchEnemy : Enemy, Damageable
{ //NOTE : Implement Melee Attack when very close to the player
    private bool canMove = true;

    public float attackDelay = 2f;
    public GameObject projectile;

    private float timeBetweenAttacks = 0f;
    // Use this for initialization

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Move();
        }
        float distance = Vector2.Distance(transform.position, target.position);
        if(distance <= attackRange)
            Attack();
    }

    public override void Attack()
    {
        if (characterAnimator.GetBool("FireBallAttack") == true)
        {
            Debug.Log("fireball");
        }

        if (timeBetweenAttacks > attackDelay)
        {
            characterAnimator.SetTrigger("FireBallAttack");

            SpawnProjectile(transform.position);

            timeBetweenAttacks = 0;

            StartCoroutine("Recharge");
        }

        timeBetweenAttacks += Time.deltaTime;
    }


    private void SpawnProjectile(Vector2 spawnPosition)
    {
        var spawn = Instantiate(projectile, spawnPosition, Quaternion.identity) as GameObject;
        spawn.transform.localScale = transform.localScale;

    }


    public override void Move()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {

            Flip(target.position.x - transform.position.x);
            Vector2 heading = target.position - transform.position;
            Vector2 direction = - heading / distance;
            float deltaSpeed = (movementSpeed+1f) * Time.deltaTime;
            transform.Translate(direction.x * deltaSpeed, 0.0f, 0.0f);

        }
        else if (distance <= detectRange && distance > attackRange)
        {
            Flip(target.position.x - transform.position.x);
            Vector2 heading = target.position - transform.position;
            Vector2 direction =  heading / distance;
            float deltaSpeed = movementSpeed * Time.deltaTime;
            transform.Translate(direction.x * deltaSpeed, 0.0f, 0.0f);
        }

        
        
    }

    public override void Hit(Weapon weapon)
    {
        int damage = weapon.physical + weapon.magical; //possibly a more complex function

        if (damage > 0)
        {
            health.ChangeHealth(-damage);
            StartCoroutine(HitFlashing(0.6f, 0.1f));
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Recharge()
    {
        canMove = false;
        yield return new WaitForSeconds(1.7f);
        canMove = true;
    }
}
