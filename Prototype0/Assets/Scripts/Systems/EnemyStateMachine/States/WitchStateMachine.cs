using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateMachine : StateMachine {

    public override void SetStates()
    {
        this.idleState = new IdleStateWitch();
        this.patrolState = new PatrolStateWitch();
        this.rangedState = new RangedStateWitch();
        this.meleeState = new MeleeStateWitch();
    }
}
