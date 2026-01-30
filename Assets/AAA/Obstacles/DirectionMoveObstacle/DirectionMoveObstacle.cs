using UnityEngine;

public class DirectionMoveObstacle : BaseObstacle
{
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private float moveSpeed = 2f;
    public enum DirectionType 
    {
        left,
        right, 
        up,
        down
    }

    public DirectionType direction = DirectionType.left;

    public ObstacleBehavior behavior = ObstacleBehavior.DirectionalMove;

    public void Start()
    {
        OpenBehavior();
    }

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        switch(direction)
        {
            case DirectionType.left:
            case DirectionType.right:
                rigidbody.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionX;
                break;
            case DirectionType.up:
            case DirectionType.down:
                rigidbody.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;
                break;
        }
      }

    public void SetDirectionType(DirectionType type)
    {
        direction = type;
    }
    void FixedUpdate()
    {
        if(isActive)
        {
            Vector3 moveDirection = Vector3.zero;
            switch (direction)
            {
                case DirectionType.left:
                    moveDirection = Vector3.left;
                    break;
                case DirectionType.right:
                    moveDirection = Vector3.right;
                    break;
                case DirectionType.up:
                    moveDirection = Vector3.up;
                    break;
                case DirectionType.down:
                    moveDirection = Vector3.down;
                    break;
            }
            rigidbody.MovePosition(rigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            
        }
    }
}
