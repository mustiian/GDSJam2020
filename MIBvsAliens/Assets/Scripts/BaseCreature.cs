using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    public Animator animator;
    
    public int cost;
    public State state;
    public Race race;

    public void PlayMoveAnimation()
    {
        animator.Play("Walk");
    }
    
    public void PlayAttackAnimation()
    {
        animator.Play("Attack", 0);
    }
    
    public void PlayDeathAnimation()
    { 
        animator.Play("Death", 0);
    }

}
