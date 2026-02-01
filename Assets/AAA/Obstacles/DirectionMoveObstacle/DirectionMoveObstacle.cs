using System.Diagnostics;
using UnityEngine;


public class DirectionMoveObstacle : BaseObstacle
{
    [SerializeField] private Animator animator;
    private bool isMoving = false;
    public enum DirectionType 
    {
        left,
        right, 
        up,
        down
    }

    public DirectionType direction = DirectionType.left;

    public ObstacleBehavior behavior = ObstacleBehavior.DirectionalMove;

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        switch(direction)
        {
            case DirectionType.left:
                animator.SetBool("moveLeft", true);
                break;
            case DirectionType.right:
                animator.SetBool("moveRight", true);
                break;
            case DirectionType.up:
                animator.SetBool("moveUp", true);
                break;
            case DirectionType.down:
                animator.SetBool("moveDown", true);
                break;
        }
        SoundManager.Instance.PlaySFX("directionMove");
      }
}
