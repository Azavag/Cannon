using UnityEngine;

public class EnemyWalkState : IEnemyState
{
    public void EnterState()
    {
        // Код при входе в состояние Walk
        Debug.Log("Entering Walk State");
    }

    public void UpdateState()
    {
        // Код для обновления состояния Walk
        // Например, движение врага
    }

    public void ExitState()
    {
        // Код при выходе из состояния Walk
        Debug.Log("Exiting Walk State");
    }
}
