using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStateWitch : IEnemyState
{
    private EnemyState enemy;

    private float throwTimer = 0f; //how much time has passed since we threw the last projectile
    private float throwCoolDown = 0.5f; //how long before throwing another projectile
    private bool canThrow = true;
    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
        enemy.CharacterAnimator.SetTrigger("Attack");

    }

    public void Execute()
    {
        enemy.LookAtTarget();
        if (!enemy.IsAttacking)
        {
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
        throwTimer = 0;
        canThrow = false;
        this.enemy.movementSpeed = 3.5f;
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
            throwCoolDown = Random.Range(1f, 1.4f);
            enemy.CharacterAnimator.SetTrigger("Attack");
        }
    }
}

