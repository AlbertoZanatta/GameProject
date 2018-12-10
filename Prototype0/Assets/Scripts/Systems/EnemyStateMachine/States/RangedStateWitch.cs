using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStateWitch : IEnemyState
{
    private EnemyState enemy;

    private float throwTimer = 0f; //how much time has passed since we threw the last projectile
    private float throwCoolDown = Random.Range(1.4f, 1.8f); //how long before throwing another projectile
    private bool canThrow = true;
    private int consecutiveAttacks = 0;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
        enemy.CharacterAnimator.SetTrigger("Attack");
        consecutiveAttacks++;
        Debug.Log("Consecutive attacks: " + consecutiveAttacks);

    }

    public void Execute()
    {
        enemy.LookAtTarget();
        if (!enemy.IsAttacking)
        {
            if (consecutiveAttacks >= 3)
            {
                Debug.Log("Going to idle!");
                consecutiveAttacks = 0;
                enemy.ForceIdling = true;
                enemy.ChangeState(enemy.stateMachine.idleState);
                return;
            }

            if (enemy.InMeleeRange)
            {
                enemy.ChangeState(enemy.stateMachine.meleeState);
            }
            else if(enemy.InThrowRange)
            {
                if(enemy.IsEnemyImpeded(true))
                {
                    enemy.ChangeState(enemy.stateMachine.idleState);
                }
                else
                {
                   enemy.characterRigidbody.velocity = Vector2.zero;
                   ThrowProjectile();
                }
            }
            else if (enemy.Target == null)
            {
                enemy.ChangeState(enemy.stateMachine.idleState);
            }

        }
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void ThrowProjectile()
    {
        throwTimer += Time.deltaTime;

        if(throwTimer >= throwCoolDown)
        {
            canThrow = true;
        }

        if(canThrow)
        {
            canThrow = false;
            throwTimer = 0;
            throwCoolDown = Random.Range(1.5f, 2.5f);
            enemy.CharacterAnimator.SetTrigger("Attack");
            consecutiveAttacks++;
        }
    }
}

