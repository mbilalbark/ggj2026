using UnityEngine;

public class FallObstacle : BaseObstacle
{ 
    public ObstacleBehavior behavior = ObstacleBehavior.Fall;

    [SerializeField] private Rigidbody rb;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    } 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenBehavior();
        }
    }
    
}
