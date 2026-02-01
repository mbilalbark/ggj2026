using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform characterCameraPoint;
    private int healtCount;
    [SerializeField] private Transform characterTransform;

    
    private Rigidbody rb;
    private MeshCollider meshCollider;
    private bool isGrounded;
    private bool isObstacle;
    private int jumpCount;
    private bool isAlive = true;

    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if (!isAlive) return;
        Move();
        Jump();
    }

    public Transform GetCharacterCameraPoint()
    {
        return characterCameraPoint;
    }

    public Transform GetCharacterPoint() 
    { 
        return characterTransform; 
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
        
        if (moveDirection.magnitude > 0)
        {
            moveDirection = moveDirection.normalized;
        }

        float currentSpeed = moveSpeed;
        if (Input.GetButtonDown("Crouch") && isGrounded)
        {
            characterTransform.localScale *= 0.5f;
        }

        if (Input.GetButtonUp("Crouch") && isGrounded)
        {
            characterTransform.localScale *= 2f;
        }

        if (Input.GetButton("Crouch") && isGrounded)
        {
            currentSpeed = moveSpeed * 0.5f;
        }
        else if (Input.GetButton("Sprint") && isGrounded)
        {
            currentSpeed = moveSpeed * 2f;
        }
        else if (!isGrounded)
        {
            currentSpeed = moveSpeed * 0.5f; 
        }

        rb.linearVelocity = new Vector3(
            moveDirection.x * currentSpeed, 
            rb.linearVelocity.y, 
            moveDirection.z * currentSpeed
        );
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpCount = 0;
                float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
                jumpCount++;
            }
            else if (jumpCount == 1)
            {
                float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
                jumpCount = 0;
            }
        }
    }   

        private void OnCollisionEnter(Collision collision)
    {
        if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isObstacle = true;
            TakeHitCharacter();
        }

        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0 && transform.position.y > 1.6f) 
        {
            isGrounded = false;
        }

        if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isObstacle = false;
        }
    }
    
    private void TakeHitCharacter()
    {
        healtCount -= 1;
        if (healtCount <= 0)
        {
            isAlive = false;
        }
        else
        {
            UIManager.Instance.TakeHit();
        }
    }
}
