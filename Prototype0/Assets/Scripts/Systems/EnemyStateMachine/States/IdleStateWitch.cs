using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateWitch : IEnemyState{

    private EnemyState enemy;

    private float idleTimer; //time that we have idled. When over threshold we switch to patrol state
    private float idleDuration = Random.Range(2f, 4f);

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
        idleTimer = 0;
        idleDuration = Random.Range(1f, 2f);
    }

    public void Execute()
    {
        if(enemy.ForceIdling)
        {
            Debug.Log("I'm force idling!");
        }
        Idle();
        if(!enemy.ForceIdling)
        {
            if (enemy.Target != null)
            {
                enemy.ChangeState(enemy.stateMachine.patrolState);
            }
        } 
    }

    public void Exit()
    {
        

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

        if (idleTimer >= idleDuration)
        {
            if(enemy.ForceIdling)
            {
                enemy.ForceIdling = false;
            }
            enemy.ChangeState(enemy.stateMachine.patrolState);
        }
    }
}
