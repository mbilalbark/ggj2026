using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    private int healtCount;

    private Rigidbody rb;
    private MeshCollider meshCollider;
    private bool isGrounded;
    private bool isObstacle;
    private int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    public void SetHealthCount(int count)
    {
        healtCount = count;
    }

    public int GetHealthCount()
    {
        return healtCount;
    }

    private void Move()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * moveInputX) + (transform.forward * moveInputZ);

        if (Input.GetButton("Crouch") && isGrounded)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed / 2, rb.linearVelocity.y, moveDirection.z * moveSpeed / 2);
            Debug.Log("Pressing c");
        }

        else if (Input.GetButton("Sprint") && isGrounded)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed * 2, rb.linearVelocity.y, moveDirection.z * moveSpeed * 2);
            Debug.Log("Pressing Left Shift");
        }
        else if (Input.GetButton("Jump") || !isGrounded)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed / 2, rb.linearVelocity.y, moveDirection.z * moveSpeed / 2);
        }
        else
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }

    }

    private void JumpCount ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpCount++;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpCount = 0;
                var jumpVelocity = new Vector2(rb.linearVelocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
                rb.linearVelocity = jumpVelocity;
                jumpCount++;
            }
            else if (jumpCount == 1)
            {
                var jumpVelocity = new Vector2(rb.linearVelocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
                rb.linearVelocity = jumpVelocity;
                jumpCount = 0;
            }
        }
    }   

    private void OnCollisionEnter(Collision collision)
    {
        if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isObstacle = true;
            healtCount -= 1;
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
