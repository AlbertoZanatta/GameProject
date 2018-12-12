using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VikingStateMachine : StateMachine
{

  
    public override void SetStates()
    {
        this.idleState = new IdleState();
        this.patrolState = new PatrolState();
        this.rangedState = new RangedState();
        this.meleeState = new VikingMeleeState();
    }



}
