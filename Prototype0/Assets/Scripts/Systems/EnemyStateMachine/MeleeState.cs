using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private float attackTimer = 0; 
    private float attackCoolDown = 3f; 
    private bool canAttack = true;

    private EnemyState enemy;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        
        Attack();
        if(enemy.InThrowRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(enemy.stateMachine.rangedState);
        }
        else if(enemy.Target == null)
        {
            enemy.ChangeState(enemy.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        attackTimer = 0;
        canAttack = true;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        
        if (canAttack)
        {
            canAttack = false;
            enemy.CharacterAnimator.SetTrigger("Attack");
        }
    }
}
