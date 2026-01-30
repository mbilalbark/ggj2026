using UnityEngine;

public class FireObstacle : BaseObstacle
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private ParticleSystem fire;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
        fire.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenBehavior();
        }
    }

}
