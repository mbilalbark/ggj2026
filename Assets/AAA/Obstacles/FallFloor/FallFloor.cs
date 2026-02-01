using UnityEngine;
using System.Collections;
public class FallFloor : BaseObstacle
{   
   [SerializeField] private Rigidbody[] fallRigidbodies;
   [SerializeField] private float fallDelay = 0.5f;
    public override void OpenBehavior()
    {   
        base.OpenBehavior();
        StartCoroutine(Fall());
    }
    private IEnumerator Fall()
    {
        foreach (Rigidbody rb in fallRigidbodies)
        {
            yield return new WaitForSeconds(fallDelay);
            rb.isKinematic = false;
        }
    }

  
}
