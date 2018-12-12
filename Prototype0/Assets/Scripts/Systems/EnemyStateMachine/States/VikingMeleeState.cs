using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingMeleeState : IEnemyState {
    public float safeDist = 0.6f;

    private float attackTimer = 0;
    private float attackCoolDown = 1.5f;

    //parameters to handle the parrying
    private float parryDuration = 1.5f;
    private float timeParrying;

    private float parryCooldown = 1.5f;
    private float parryRecharge = 0f;

    private bool canParry = true;

    //parameters for handling the jump
    private float jumpTakeOffSpeed = 7f;
    private float horizontalJumpTakeOff = 2f;


    private bool canAttack = true;
    private EnemyState enemy;

    public void Enter(EnemyState enemy)
    {
        this.enemy = enemy;
    }
    //  enemy.CharacterAnimator.SetTrigger("Attack");
    public void Execute()
    {
        if (enemy.InOverlapDist)
        {
            if(!IsAttacking() && !IsParrying())
            {
                Jump();
            }   
        }

        enemy.characterAnimator.SetBool("grounded", enemy.OnGround);

        
        if(enemy.OnGround)
        {
            enemy.LookAtTarget();
            if (enemy.InMeleeRange)
            {
                Attack();
                Parry();
                if (canAttack)
                {
                    StartAttack();
                }
                else if (canParry)
                {
                    StartParry();
                }

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
        if(!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCoolDown)
            {
                if(!IsParrying())
                {
                    canAttack = true;
                    attackTimer = 0;
                }  
            }
        }
    }
    private void StartAttack()
    {
        enemy.CharacterAnimator.SetTrigger("Attack");
        canAttack = false;
    }
    private void StartParry()
    {
        enemy.CharacterAnimator.SetBool("Parry", true);
    }

    private bool IsParrying()
    {
        return enemy.characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("parrying_animation");
    }

    private bool IsAttacking()
    {
        return enemy.characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("attacking_animation");
    }

    private void StopParry()
    {
        enemy.CharacterAnimator.SetBool("Parry", false);
    }

    private void Parry()
    {
        if(IsParrying())
        {
            timeParrying += Time.deltaTime;
            if(timeParrying >= parryDuration)
            {
                StopParry();
                canParry = false;
                timeParrying = 0f;
            }
        }
        else if(!canParry)
        {
            parryRecharge += Time.deltaTime;
            if(parryRecharge >= parryCooldown)
            {
                if(!IsAttacking())
                {
                    canParry = true;
                    parryRecharge = 0f;
                }  
            }
        }
    }

    private void Jump()
    {
        enemy.characterRigidbody.velocity = new Vector2(horizontalJumpTakeOff*enemy.transform.localScale.x, jumpTakeOffSpeed);
    }
}
