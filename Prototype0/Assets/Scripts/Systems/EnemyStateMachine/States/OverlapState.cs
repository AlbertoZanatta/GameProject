using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapState : IEnemyState {

    private EnemyState enemy;
    
    public float safeDist = 1f;
    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;

    }

    public void Execute()
    {
        enemy.LookAtTarget();
        enemy.MoveBack();
       
        if(enemy.InMeleeRange)
        {
            float distance = Mathf.Abs(Vector3.Distance(enemy.transform.position, enemy.Target.transform.position));
            if (distance >= safeDist)
            {
                enemy.ChangeState(enemy.stateMachine.meleeState);
            }
        }
      
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
    }
}
