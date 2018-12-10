using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateMachine : StateMachine {

    public override void SetStates()
    {
        this.idleState = new IdleState();
        this.patrolState = new PatrolState();
        this.rangedState = new RangedState();
        this.meleeState = new MeleeState();
    }
}
