using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    protected EnemyData enemyData;
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyAnimator enemyAnimator;
    protected AgentMover agentMover;

    private int enemyHealth;
    private Transform targetTransform;
    bool isFollowPlayer = false;

    

    virtual protected void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        agentMover = GetComponent<AgentMover>();
        enemyStateMachine = new EnemyStateMachine();
    }
    private void OnEnable()
    {
        agentMover.OnStartJump.AddListener(SetJumpState);
        agentMover.OnLand.AddListener(TransitionFromJumpToWalk);
    }
    private void OnDisable()
    {
        agentMover.OnStartJump.RemoveListener(SetJumpState);
        agentMover.OnLand.AddListener(TransitionFromJumpToWalk);

    }
    virtual protected void Start()
    {
        enemyStateMachine.Initialize(enemyStateMachine.idleState);
        enemyAnimator.SetIdleAnimation();
        InitEnemyStats();
    }

    private void InitEnemyStats()
    {
        agentMover.InitAgentSpeed(enemyData.enemySpeed);
        agentMover.InitAgentStoppingDistance(enemyData.enemyMeleeAttackRange);
    }

    private void Update()
    {
        enemyStateMachine.Update();

    }
    private void LateUpdate()
    {
        FollowPlayer();     
    }

    private void SetIdleState()
    {
        enemyStateMachine.TransitionTo(enemyStateMachine.idleState);
        enemyAnimator.SetIdleAnimation();
    }
    private void SetWalkState()
    {
        enemyStateMachine.TransitionTo(enemyStateMachine.walkState);
        enemyAnimator.SetWalkAnimation();
    }
    private void SetJumpState()
    {
        enemyStateMachine.TransitionTo(enemyStateMachine.jumpState);
        enemyAnimator.SetJumpAnimationState(true);
    }   

    private void TransitionFromJumpToWalk()
    {
        enemyAnimator.SetJumpAnimationState(false);
        SetWalkState();
    }
    public void TransitionFromWalkToAttack()
    {     
        if (!agentMover.IsStopped())
            agentMover.StopMovement();

        SetAttackState();
    }
    private void SetAttackState()
    {
        enemyStateMachine.TransitionTo(enemyStateMachine.attackState);
        enemyAnimator.SetAttackAnimationState(true);
    }


    public void TransitionFromAttackToWalk()
    {
        enemyStateMachine.TransitionTo(enemyStateMachine.walkState);
        agentMover.StartMovement();
        enemyAnimator.SetAttackAnimationState(false);
    }

    public void StartFollowPlayer(Transform playerTransform)
    {
        targetTransform = playerTransform;
        isFollowPlayer = true;
        SetWalkState();
    }
    public void StopFollowPlayer()
    {
        isFollowPlayer = false;
    }

    private void FollowPlayer()
    {
        if (!isFollowPlayer)
            return;
        agentMover.SetDestination(targetTransform.position);
    }

    public void GetDamage(int damageValue)
    {
        enemyHealth -= damageValue;
        StopFollowPlayer();
        if (enemyHealth < 0 )
            Death();
    }


    private void Death()
    {
        Destroy(gameObject);
    }
}
