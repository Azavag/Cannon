using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine
{
    public IEnemyState CurrentState { get; private set; }
    public EnemyIdleState idleState;
    public EnemyWalkState walkState;
    public EnemyJumpState jumpState;
    public EnemyAttackState attackState;

    public EnemyStateMachine()
    {
        this.idleState = new EnemyIdleState();
        this.walkState = new EnemyWalkState();
        this.jumpState = new EnemyJumpState();
        this.attackState = new EnemyAttackState();
    }

    public void Initialize(IEnemyState startingState)
    {
        CurrentState = startingState;
        startingState.EnterState();
    }
    public void TransitionTo(IEnemyState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
}
