using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField]
    private GameObject rangeAttackProjectile;
    [SerializeField]
    private Transform rangeAttackPlace;

    override protected IEnumerator PerformAttack()
    {
        enemy.TransitionFromWalkToAttack();
        yield return new WaitForSeconds(attackDelay);
        SpawnProjectile();

        //if (IsRaycastHitPlayer())
        //{
        //    Debug.Log("ÓÐÎÍ = " + attackDamage);
        //    OnAttackHit();
        //}
    }

    [ContextMenu("SpawnProjectile")]
    private void SpawnProjectile()
    {
        GameObject projectileInstanse = Instantiate(rangeAttackProjectile, 
            rangeAttackPlace.position,
            Quaternion.identity,
            transform);
        

    }
}
