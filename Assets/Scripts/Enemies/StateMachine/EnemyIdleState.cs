using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState()
    {
        // Код при входе в состояние Idle
        Debug.Log("Entering Idle State");
    }

    public void UpdateState()
    {
        // Код для обновления состояния Idle
        // Например, проверка условий для перехода в другое состояние
    }

    public void ExitState()
    {
        // Код при выходе из состояния Idle
        Debug.Log("Exiting Idle State");
    }

    public void SetAnimationState(Animator animator)
    {
        animator.SetBool("isIdle", true);
    }
}


