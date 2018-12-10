using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    public float safeDist = 0.6f;

    private float attackTimer = 0; 
    private float attackCoolDown = 1.5f; 
    private bool canAttack = true;
    private EnemyState enemy;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        enemy.LookAtTarget();
        if (enemy.InOverlapDist)
        {
            //Debug.Log("Moving back!!");
            enemy.MoveBack();
        }
        else
        {
            if(enemy.InMeleeRange)
            {
                Attack();
            }
            else if (enemy.InThrowRange)
            {
                enemy.ChangeState(enemy.stateMachine.rangedState);
            }
            else if (enemy.Target == null)
            {
                enemy.ChangeState(enemy.stateMachine.idleState);
            }
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
