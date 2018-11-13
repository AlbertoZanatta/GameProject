using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateWitch : IEnemyState
{
    private float attackTimer; 
    private float attackCoolDown = 3f; 
    private bool cornered = false;

    private EnemyState enemy;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if(!cornered)
        {
            enemy. MoveBack();
        }
        else
        {
            enemy.Move();
        }
        
        if(enemy.InThrowRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(enemy.stateMachine.rangedState);
            cornered = false;
        }
        else if(enemy.Target == null)
        {
            enemy.ChangeState(enemy.stateMachine.idleState);
        }
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            cornered = true;
        }
    }
}
