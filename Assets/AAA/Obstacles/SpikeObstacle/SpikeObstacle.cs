using UnityEngine;
using static DirectionMoveObstacle;

public class SpikeObstacle : BaseObstacle
{
    [SerializeField] private float moveSpeed = 2f;
    public enum SpikeDirection
    {

        up
    }



    public SpikeDirection direction;
    public ObstacleBehavior behavior = ObstacleBehavior.Move;
    void Update()
    {
        OpenBehavior();
    }

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        switch (direction)
        {
            case direction.up:
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;
                break;
        }
    }
    public void SetDirectionType(SpikeDirection type)
    {
        direction = type;
    }
    void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 moveDirection = Vector3.zero;
            switch (direction)
            {

                case SpikeDirection.up:
                    moveDirection = Vector3.up;
                    break;

            }


        }
    }
}
