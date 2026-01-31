using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    
    private Transform characterCameraPoint;
    private CharacterController characterController;
    private bool isFollowing = false;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isFollowing)
        {
            TryFindCharacter();
            return;
        }

        if (characterController == null || characterCameraPoint == null)
        {
            isFollowing = false;
            return;
        }

        FollowCharacter();
        HandleMouseLook();
    }

    void TryFindCharacter()
    {
        characterController = FindObjectOfType<CharacterController>();
        
        if (characterController != null)
        {
            characterCameraPoint = characterController.GetCharacterCameraPoint();
            
            if (characterCameraPoint != null)
            {
                isFollowing = true;
                Cursor.lockState = CursorLockMode.Locked;
                yRotation = characterController.transform.eulerAngles.y;
            }
        }
    }

    void FollowCharacter()
    {
        transform.position = characterCameraPoint.position;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        // Kameranın hem yatay hem dikey rotasyonunu ayarla
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        // Karakteri de aynı yatay rotasyona ayarla (hareket yönü için)
        characterController.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void SetCharacter(CharacterController controller)
    {
        characterController = controller;
        characterCameraPoint = controller.GetCharacterCameraPoint();
        yRotation = controller.transform.eulerAngles.y;
        isFollowing = true;
    }

    public void ClearCharacter()
    {
        characterController = null;
        characterCameraPoint = null;
        isFollowing = false;
    }
}