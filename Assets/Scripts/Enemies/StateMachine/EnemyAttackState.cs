using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void EnterState()
    {
        Debug.Log("���� � ��������� �����");
    }

    public void ExitState()
    {
        Debug.Log("����� �� ��������� �����");
    }

    public void UpdateState()
    {
        
    }
}
