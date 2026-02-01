using UnityEngine;

public class ObstacleCapsule : MonoBehaviour
{
   [SerializeField] private BaseObstacle obstacle;

    public void Initialize(bool isActive)
    {
        obstacle.SetIsActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && obstacle.GetIsActive())
        { Debug.Log("Player entered ObstacleCapsule trigger.");
            if (obstacle == null) return;
            obstacle.gameObject.SetActive(true);
            obstacle.OpenBehavior();
        }
    }
}
