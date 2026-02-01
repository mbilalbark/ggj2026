using UnityEngine;

public class FireObstacle : BaseObstacle
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private GameObject[] lights;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
            fire.Play();
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
        }
       
    }
}
