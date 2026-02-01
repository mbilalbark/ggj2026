using UnityEngine;
using static DirectionMoveObstacle;

public class SpikeObstacle : BaseObstacle
{
    [SerializeField] private Animator animator;

     public override void OpenBehavior()
    {
        base.OpenBehavior();
        if (animator != null)
        {
            animator.SetBool("openspike", true);
            Debug.Log("SpikeObstacle OpenBehavior called, animator set to openspike true.");
        }
    }
}