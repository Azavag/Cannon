using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState()
    {
        // ��� ��� ����� � ��������� Idle
        Debug.Log("Entering Idle State");
    }

    public void UpdateState()
    {
        // ��� ��� ���������� ��������� Idle
        // ��������, �������� ������� ��� �������� � ������ ���������
    }

    public void ExitState()
    {
        // ��� ��� ������ �� ��������� Idle
        Debug.Log("Exiting Idle State");
    }

    public void SetAnimationState(Animator animator)
    {
        animator.SetBool("isIdle", true);
    }
}


