using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private EnemyState enemy;

    private float idleTimer; //time that we have idled. When over threshold we switch to patrol state
    private float idleDuration = 3f;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Idle();
        if(enemy.Target != null)
        {
            enemy.ChangeState(enemy.stateMachine.patrolState);
        }
    }

    public void Exit()
    {
        idleTimer = 0;

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.CharacterAnimator.SetBool("grounded", true);
        enemy.CharacterAnimator.SetFloat("VelocityX", 0);
        enemy.characterRigidbody.velocity = Vector2.zero;

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            enemy.ChangeState(enemy.stateMachine.patrolState);
        }
    }
}

