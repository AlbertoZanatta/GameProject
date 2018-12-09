﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public IEnemyState idleState;
    public IEnemyState patrolState;
    public IEnemyState rangedState;
    public IEnemyState meleeState;

    public void SetStates(IEnemyState idleState, IEnemyState patrolState, IEnemyState rangedState, IEnemyState meleeState)
    {
        this.idleState = idleState;
        this.patrolState = patrolState;
        this.rangedState = rangedState;
        this.meleeState = meleeState;
    }
}
