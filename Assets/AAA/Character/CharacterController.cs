using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private Rigidbody rb;
    private MeshCollider meshCollider;
    private bool isGrounded;
    private bool isObstacle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();

    }

    void Update()
    {
        Move();
        Jump();
        CheckDie();
    }

    private void Move()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * moveInputX) + (transform.forward * moveInputZ);

        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Vector2 jumpVelocity = new Vector2(rb.linearVelocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
                rb.linearVelocity = jumpVelocity;
            }
        }
    }

    private void CheckDie ()
    {
        if (isObstacle)
        {
            Debug.Log("YANDIN");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isObstacle = true;
        }

        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "obstacleLayer")
        {
            isObstacle = false;
        }

        if (LayerMask.LayerToName(collision.gameObject.layer) == "groundLayer")
        {
            isGrounded = false; 
        }
    }
}
