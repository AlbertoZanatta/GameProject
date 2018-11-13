using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStateWitch : IEnemyState
{
    private EnemyState enemy;

    private float throwTimer = 0f; //how much time has passed since we threw the last projectile
    private float throwCoolDown = 1.5f; //how long before throwing another projectile
    private bool canThrow = true;
    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if(enemy.InMeleeRange)
        {
            enemy.ChangeState(enemy.stateMachine.meleeState);
        }

        ThrowProjectile();

        if(enemy.Target == null)
        {
            enemy.ChangeState(enemy.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        throwTimer = 0;
        canThrow = false;
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
            Debug.Log("Witch Attack!");
            canThrow = false;
            throwTimer = 0;
            enemy.CharacterAnimator.SetTrigger("Attack");
        }
    }
}

