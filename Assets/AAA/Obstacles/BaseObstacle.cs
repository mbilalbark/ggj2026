using UnityEngine;
using UnityEngine.Rendering;

public class BaseObstacle : MonoBehaviour
{ 
    public enum ObstacleBehavior
    {
        Fall,
        Move,
        Rotate,
        DirectionalMove
    }

    protected bool isActive = false;
    public bool GetIsActive()
    {
        return isActive;
    }
    public void SetIsActive(bool value)
    {
        isActive = value;
    }
    public virtual void OpenBehavior()
    {
        isActive = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") && isActive)
        {
            SetIsActive(false);
        }

    }


}
