using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyState : EnemyState {
    protected override void SetStates()
    {
        if(stateMachine != null)
        {
            stateMachine.SetStates(new IdleState(), new PatrolState(), new RangedState(), new MeleeState());
        }
    }
}
