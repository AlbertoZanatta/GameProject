using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateWitch : IEnemyState
{
    private EnemyState enemy;
    private float keepDistance;
    private float originalSpeed;
    private float meleeTimer = 0;
    private float meleeDuration = Random.Range(4f, 5f);

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
        originalSpeed = this.enemy.movementSpeed;
        this.enemy.movementSpeed = Random.Range(2.5f, 5.5f);
        keepDistance = Random.Range(2.5f, 4f);
        meleeTimer = 0;
        meleeDuration = Random.Range(4f, 5f);
    }

    public void Execute()
    {
        //enemy.LookAtTarget();
        meleeTimer += Time.deltaTime;
        if(meleeTimer >= meleeDuration)
        {
            float odds = Random.Range(0f, 1f);
            if(odds < 0.1f)
            {
                enemy.ChangeState(enemy.stateMachine.rangedState);
                return;
            }
            else
            {
                enemy.ForceIdling = true;
                enemy.ChangeState(enemy.stateMachine.idleState);
                return;

            }
            

        }
        if(!enemy.IsAttacking)
        {
            if (enemy.InMeleeRange)
            {
                Debug.Log("Enemy too close");
                if (enemy.IsGroundImpeded(false) || enemy.OnPlatformEdge(false) || enemy.IsEnemyImpeded(false))
                {
                    enemy.ChangeDirection();

                }
                enemy.MoveBack();
            }
            else if (enemy.InThrowRange && enemy.targetDistance() <= keepDistance)
            {
                Debug.Log("Enemy moderately close");
                if (!enemy.IsGroundImpeded(false) && !enemy.OnPlatformEdge(false) && !enemy.IsEnemyImpeded(false))
                    enemy.MoveBack();
                else
                    enemy.characterRigidbody.velocity = Vector2.zero;
            }
            else if (enemy.InThrowRange && enemy.targetDistance() >= keepDistance)
            {
                Debug.Log("Enemy far away");
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
        this.enemy.movementSpeed = originalSpeed;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
