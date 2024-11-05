using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public event Action HealthChanged;

    private const int minHealth = 0;
    private const int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MinHealth => minHealth;
    public int MaxHealth => maxHealth;

    private void OnEnable()
    {
        //EnemyMeleeAttack.AttackHit += EnemyMeleeAttack_OnEnemyAttacking;
    }
    private void OnDisable()
    {
        //EnemyMeleeAttack.AttackHit -= EnemyMeleeAttack_OnEnemyAttacking;
    }
    private void Start()
    {
        Restore();
    }
    private void EnemyMeleeAttack_OnEnemyAttacking(int damageValue)
    {
        Decrement(damageValue);
    }
   
    public void Increment(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        UpdateHealth();
    }
    public void Decrement(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        UpdateHealth();
    }
    public void Restore()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke();
    }
}
