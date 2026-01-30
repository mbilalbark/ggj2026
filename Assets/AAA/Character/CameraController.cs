using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Mouse imlecini ekranın ortasına kilitler ve gizler
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse girdilerini alıyoruz
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukarı-Aşağı bakış (X ekseni etrafında dönüş)
        xRotation -= mouseY;
        // 90 derece kısıtlaması (Boynun kırılmaması için)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameranın yerel rotasyonunu güncelle
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin gövdesini sağa-sola döndür (Y ekseni etrafında)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}