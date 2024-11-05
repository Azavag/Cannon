using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AgentMover : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private NavMeshAgent _agent;
    private bool _onNavMeshLink = false;
    public event Action<float> OnSpeedChanged;

    [Header("Settings")]
    [SerializeField]
    private float _jumpDuration = 0.8f;

    public UnityEvent OnLand, OnStartJump;
   
    private void Start()
    {
        _agent.autoTraverseOffMeshLink = false;
        StartMovement();
    }

    public void InitAgentSpeed(float speed)
    {
        _agent.speed = speed;
    }
    public void InitAgentStoppingDistance(float stopDistance)
    {
        _agent.stoppingDistance = stopDistance;
    }
   
    public void StopMovement()
    {
        _agent.isStopped = true;
    }
    public void StartMovement()
    {
        _agent.isStopped = false;
    }
    public bool IsStopped()
    {
        return _agent.isStopped;
    }


    public void SetDestination(Vector3 destination)
    {
        if (_onNavMeshLink)
            return;

        _agent.destination = destination;
    }

    private void Update()
    {
        OnSpeedChanged?.Invoke(
            Mathf.Clamp01(_agent.velocity.magnitude / _agent.speed));
        
        if (_agent.isOnOffMeshLink && _onNavMeshLink == false)
        {
            StartNavMeshLinkMovement();
        }
        if (_onNavMeshLink)
        {
            FaceTarget(_agent.currentOffMeshLinkData.endPos);
        }
    }

    private void StartNavMeshLinkMovement()
    {
        _onNavMeshLink = true;
        NavMeshLink link = (NavMeshLink)_agent.navMeshOwner;
        Spline spline = link.GetComponentInChildren<Spline>();

        PerformJump(link, spline);
    }

    private void PerformJump(NavMeshLink link, Spline spline)
    {
        bool reverseDirection = CheckIfJumpingFromEndToStart(link);
        StartCoroutine(MoveOnOffMeshLink(spline, reverseDirection));

        OnStartJump?.Invoke();
    }

    private bool CheckIfJumpingFromEndToStart(NavMeshLink link)
    {
        Vector3 startPosWorld
            = link.gameObject.transform.TransformPoint(link.startPoint);
        Vector3 endPosWorld
            = link.gameObject.transform.TransformPoint(link.endPoint);

        float distancePlayerToStart 
            = Vector3.Distance(_agent.transform.position, startPosWorld);
        float distancePlayerToEnd 
            = Vector3.Distance(_agent.transform.position, endPosWorld);


        return distancePlayerToStart > distancePlayerToEnd;
    }

    private IEnumerator MoveOnOffMeshLink(Spline spline, bool reverseDirection)
    {
        float currentTime = 0;
        Vector3 agentStartPosition = _agent.transform.position;

        while (currentTime < _jumpDuration)
        {
            currentTime += Time.deltaTime;

            float amount = Mathf.Clamp01(currentTime / _jumpDuration);
            amount = reverseDirection ? 1 - amount : amount;

            _agent.transform.position =
                reverseDirection ?
                spline.CalculatePositionCustomEnd(amount, agentStartPosition)
                : spline.CalculatePositionCustomStart(amount, agentStartPosition);

            yield return new WaitForEndOfFrame();
        }

        _agent.CompleteOffMeshLink();

        OnLand?.Invoke();
        yield return new WaitForSeconds(0.1f);
        _onNavMeshLink = false;

    }


    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation 
            = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation 
            = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

}
