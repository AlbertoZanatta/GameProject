using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy, Damageable {
    public LayerMask playerMask;
    
    private Weapon weapon;
    [SerializeField] private Utils.MinMax attackCooldown = new Utils.MinMax(1.5f, 3f);
    private bool canAttack = true;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        weapon = new Weapon(2, 0, Weapon.WeaponType.Axe);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public override void Attack()
    {
        Vector3 attackDirection = facingRight ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, playerMask);
        Debug.DrawRay(transform.position, attackDirection * attackRange, Color.red, 0.2f);
        if (hit.collider != null)
        {
            GameObject attacked = hit.transform.gameObject;
            Damageable damageable = attacked.GetComponent<Damageable>();
            if(damageable != null)
            {
                damageable.Hit(weapon);
            }

        }
    }

    public override void Move()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if(distance <= stopAttackRange)
        {
            HandleAttack();

        }
        else if (distance <= detectRange)
        {
            Flip(target.position.x - transform.position.x);
            Vector2 heading = target.position - transform.position;
            Vector2 direction = heading / distance;
            float deltaSpeed = movementSpeed * Time.deltaTime;
            transform.Translate(direction.x * deltaSpeed, 0.0f, 0.0f);
        }
    }

    private void HandleAttack()
    {
        if(canAttack && !IsAttacking())
        {
            characterAnimator.SetTrigger("Attack");
            //Attack();
            StartCoroutine("AttackCooldown");
        }
    }

    private bool IsAttacking()
    {
        return characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("enemy_melee_attack_animation");
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        float coolDown = attackCooldown.GetRand();
        Debug.Log("Attack cooldown: " + coolDown);
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }

    public override void Hit(Weapon weapon)
    {
        int physicalDamage = weapon.physical;
        health.ChangeHealth(-physicalDamage);
        StartCoroutine(HitFlashing(1f, 0.1f));
    }
}
