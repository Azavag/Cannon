using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;

    protected void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void SetIdleAnimation()
    {
        animator.SetBool("isIdle", true);
    }

    public void SetWalkAnimation()
    {
        animator.SetBool("isWalk", true);
    }
    

    public void SetJumpAnimationState(bool state)
    {
        if (state)
            animator.SetTrigger("jump");
        else
            animator.ResetTrigger("jump");
    }

    public void SetAttackAnimationState(bool state)
    {
        if (state)
            animator.SetTrigger("attack");
        else
            animator.ResetTrigger("attack");
    }
}
