using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public int enemySpeed;
    public int enemyDamage;
    public float enemyMeleeAttackRange;

}
