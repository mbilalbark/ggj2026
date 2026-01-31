using UnityEngine;

public class ObstacleCapsule : MonoBehaviour
{
   [SerializeField] private BaseObstacle obstacle;
   [SerializeField] private Animator animator;

    public void Initialize(bool isActive)
    {
        obstacle.SetIsActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && obstacle.GetIsActive())
        {
            animator.SetBool("open", true);
            obstacle.gameObject.SetActive(true);
            obstacle.OpenBehavior();
        }
    }
}
