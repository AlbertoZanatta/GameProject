using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private EnemyState enemy;

    private float throwTimer; //how much time has passed since we threw the last projectile
    private float throwCoolDown = 3f; //how long before throwing another projectile
    private bool canThrow = false;
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

        if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(enemy.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        throwTimer = 0;
        throwCoolDown = 0;
        canThrow = false;
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
}

