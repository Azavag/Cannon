using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void EnterState()
    {
        Debug.Log("Вход в состояние атаки");
    }

    public void ExitState()
    {
        Debug.Log("Выход из состояния атаки");
    }

    public void UpdateState()
    {
        
    }
}
