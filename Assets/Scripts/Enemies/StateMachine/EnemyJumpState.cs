using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : IEnemyState
{
    public void EnterState()
    {
        // Код при входе в состояние Jump
        Debug.Log("Entering Jump State");
    }

    public void UpdateState()
    {
        // Код для обновления состояния Jump
        // Например, проверка приземления
    }

    public void ExitState()
    {
        // Код при выходе из состояния Jump
        Debug.Log("Exiting Jump State");
    }
}

