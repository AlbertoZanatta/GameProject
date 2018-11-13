using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyState enemy;

    private float patrolTimer = 0;
    private float patrolDuration = 10f;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();

        if(enemy.Target != null)
        {
           enemy.ChangeState(enemy.stateMachine.rangedState);
        }
     
    }

    public void Exit()
    {
        patrolTimer = 0;
        patrolDuration = 10f;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }


    private void Patrol()
    { 

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(enemy.stateMachine.idleState);
        }

    }
}

