using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine: MonoBehaviour
{

    public IEnemyState idleState;
    public IEnemyState patrolState;
    public IEnemyState rangedState;
    public IEnemyState meleeState;

    public abstract void SetStates();
}
