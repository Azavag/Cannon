using UnityEngine;

public class EnemyWalkState : IEnemyState
{
    public void EnterState()
    {
        // ��� ��� ����� � ��������� Walk
        Debug.Log("Entering Walk State");
    }

    public void UpdateState()
    {
        // ��� ��� ���������� ��������� Walk
        // ��������, �������� �����
    }

    public void ExitState()
    {
        // ��� ��� ������ �� ��������� Walk
        Debug.Log("Exiting Walk State");
    }
}
