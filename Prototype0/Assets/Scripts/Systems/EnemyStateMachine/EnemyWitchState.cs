using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWitchState : EnemyState {

    public GameObject fireballPrefab;

    protected override void SetStates()
    {
        stateMachine.SetStates(new IdleState(), new PatrolState(), new RangedStateWitch(), new MeleeStateWitch());
    }

    public void ThrowFireball()
    {
        Throw(fireballPrefab);
    }

    
}
