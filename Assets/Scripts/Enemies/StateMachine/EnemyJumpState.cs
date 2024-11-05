using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : IEnemyState
{
    public void EnterState()
    {
        // ��� ��� ����� � ��������� Jump
        Debug.Log("Entering Jump State");
    }

    public void UpdateState()
    {
        // ��� ��� ���������� ��������� Jump
        // ��������, �������� �����������
    }

    public void ExitState()
    {
        // ��� ��� ������ �� ��������� Jump
        Debug.Log("Exiting Jump State");
    }
}

