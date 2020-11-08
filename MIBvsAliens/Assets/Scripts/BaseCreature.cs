using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    public Animator animator;
    
    public int cost;
    public State state;
    public Race race;

    private void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        PlayMoveAnimation();
    }

    public void PlayMoveAnimation()
    {
        if (state == State.Moving || state == State.MovingBack)
            animator.Play("Walk");
    }
    
    public void PlayAttackAnimation()
    {
        if (state == State.Fighting)
            animator.Play("Attack");
    }
    
    public float PlayDeathAnimation()
    {
        if (state == State.Dying)
        {
            animator.Play("Death");
            var currentAnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
            return currentAnimatorClipInfo[0].clip.length;
        }

        return 0;
    }
}
