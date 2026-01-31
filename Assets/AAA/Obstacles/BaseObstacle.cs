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

    protected bool isActive = true;
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

}
