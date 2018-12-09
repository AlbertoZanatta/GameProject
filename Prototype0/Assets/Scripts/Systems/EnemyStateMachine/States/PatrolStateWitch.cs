using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateWitch : IEnemyState {

    private EnemyState enemy;

    private float patrolTimer;
    private float patrolDuration;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
        patrolTimer = 0;
        patrolDuration = Random.Range(5f, 10f);
    }

    public void Execute()
    {
      
        Patrol(); //Adjust timers and counters

        if(enemy.OnPlatformEdge(true) || enemy.IsGroundImpeded(true)) //Change direction if enemy is approaching ed of platform or is impeded
        {
          
            enemy.ChangeDirection();
        }

        if(enemy.IsEnemyImpeded(true)) //If enemy finds another enemy on his way, we introduce a bit of random behavior!
        {
            float odds = Random.Range(0f, 1f);
            if(odds > 0.4f)
            {
                enemy.ChangeDirection();
            }
            else
            {
                enemy.ChangeState(enemy.stateMachine.idleState);
            }

        }

        enemy.Move(); //Move the enemy in the current direction

        if (enemy.Target != null && enemy.InThrowRange) //Change to Ranged state if player has been detected and is in range
        {
            enemy.ChangeState(enemy.stateMachine.rangedState);
        }

    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
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
