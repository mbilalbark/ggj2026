using System;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("groundLayer"))
        {
            SoundManager.Instance.PlaySFX("fall");
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
