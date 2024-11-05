using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    protected EnemyData enemyData;
    protected Enemy enemy;

    [Header("Raycast settings")]
    [SerializeField]
    private Transform eyeTransform; // Трансформ, представляющий глаза противника
    [SerializeField]
    private float detectionRange = 1.0f; // Максимальная дистанция обнаружения
    [SerializeField]
    private LayerMask obstacleLayerMask;
    [SerializeField]
    private float raycastCheckInterval = 0.1f;
    private float lastRaycastCheckTime = 0;

    [Header("Attack")]
    protected bool isAttacking;
    [SerializeField]
    protected float attackCooldown = 1f;
    protected float currentAttackCooldown;
    protected int attackDamage;
    [SerializeField]
    protected float attackDelay = 0.5f;

    public static event Action<int> AttackHit;

    virtual protected void Start()
    {
        enemy = GetComponent<Enemy>();
        attackDamage = enemyData.enemyDamage;
        RefreshAttackCooldown();
    }
    protected void Update()
    {
        if (Time.time - lastRaycastCheckTime >= raycastCheckInterval)
        {
            lastRaycastCheckTime = Time.time;
            if (IsRaycastHitPlayer())
                StartAttack();
        }

        if (isAttacking)
            AttackCooldownTimer();
    }
    protected void AttackCooldownTimer()
    {
        currentAttackCooldown -= Time.deltaTime;
        if (currentAttackCooldown <= 0)
            StopAttack();

    }


    virtual protected void StartAttack()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        StartCoroutine(PerformAttack());
    }

    virtual protected IEnumerator PerformAttack()
    {
        enemy.TransitionFromWalkToAttack();
        yield return new WaitForSeconds(attackDelay);
        if (IsRaycastHitPlayer())
        {
            Debug.Log("УРОН = " + attackDamage);
            OnAttackHit();
        }
    }
    protected void StopAttack()
    {
        enemy.TransitionFromAttackToWalk();
        isAttacking = false;
        RefreshAttackCooldown();
    }

    protected bool IsRaycastHitPlayer()
    {
        Vector3 rayDirection = eyeTransform.forward;
        RaycastHit hit;
        if (Physics.Raycast(eyeTransform.position, rayDirection, out hit, detectionRange, obstacleLayerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    protected void RefreshAttackCooldown()
    {
        currentAttackCooldown = attackCooldown;
    }
    protected void OnAttackHit()
    {
        AttackHit?.Invoke(attackDamage);
    }

}
