using UnityEngine;
using System.Collections;

public class MaskController : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public float distanceFromCamera = 2f;
    public float heightOffset = 0f;
    public float followSpeed = 5f;
    
    [Header("Troll Ayarları")]
    public float minAppearInterval = 5f;
    public float maxAppearInterval = 15f;
    public float appearDuration = 2f;
    public float jumpScareChance = 0.3f;
    
    [Header("Hareket Ayarları")]
    public float wobbleSpeed = 2f;
    public float wobbleAmount = 0.1f;
    public float rotationWobble = 5f;
    
    [Header("Rotasyon Ayarı")]
    public float yRotationOffset = 180f;
    
    private Transform cameraTransform;
    private bool isVisible = false;
    private bool isFollowing = false;
    private bool isCameraFound = false;
    private Vector3 targetPosition;
    private Vector3 appearOffset = Vector3.zero;
    [SerializeField]
    private Animator animator;
    
    

    void Start()
    {
        SetVisible(false);
    }

    void Update()
    {
        if (!isCameraFound)
        {
            TryFindCamera();
            return;
        }

        if (cameraTransform == null)
        {
            isCameraFound = false;
            return;
        }

        if (!isFollowing) return;
        
        FollowAndLook();
    }

    void TryFindCamera()
    {
        var mainCam = Camera.main;
        if (mainCam != null)
        {
            cameraTransform = mainCam.transform;
            isCameraFound = true;
            StartCoroutine(TrollRoutine());
            return;
        }

        var camController = FindObjectOfType<CameraController>();
        if (camController != null)
        {
            cameraTransform = camController.transform;
            isCameraFound = true;
            StartCoroutine(TrollRoutine());
        }
    }

    void FollowAndLook()
    {
        // Hedef pozisyonu hesapla - kameranın önünde
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f; // Yatay düzlemde tut
        camForward.Normalize();
        
        Vector3 camRight = cameraTransform.right;
        
        targetPosition = cameraTransform.position 
            + camForward * distanceFromCamera 
            + camRight * appearOffset.x 
            + Vector3.up * (heightOffset + appearOffset.y);
        
        // Wobble ekle
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        float wobbleY = Mathf.Cos(Time.time * wobbleSpeed * 1.3f) * wobbleAmount;
        targetPosition += camRight * wobbleX + Vector3.up * wobbleY;
        
        // Pozisyonu güncelle
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        
        // Kameraya bak
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0f; // Sadece yatay düzlemde dönsün
        
        if (directionToCamera.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            targetRotation *= Quaternion.Euler(0f, yRotationOffset, 0f);
            
            // Wobble rotasyonu
            float rotWobble = Mathf.Sin(Time.time * wobbleSpeed * 0.7f) * rotationWobble;
            targetRotation *= Quaternion.Euler(0f, 0f, rotWobble);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
        }
    }

    void SetInitialPositionAndRotation()
    {
        // Direkt kameranın önüne yerleştir
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();
        
        Vector3 camRight = cameraTransform.right;
        
        transform.position = cameraTransform.position 
            + camForward * distanceFromCamera 
            + camRight * appearOffset.x 
            + Vector3.up * (heightOffset + appearOffset.y);
        
        // Kameraya baktır
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0f;
        
        if (directionToCamera.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(directionToCamera) * Quaternion.Euler(0f, yRotationOffset, 0f);
        }
    }

    IEnumerator TrollRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minAppearInterval, maxAppearInterval);
            yield return new WaitForSeconds(waitTime);
            
            if (isCameraFound && cameraTransform != null)
            {
                SoundManager.Instance.PlaySFX("mask");
                yield return StartCoroutine(AppearAndTroll());
            }
        }
    }

    IEnumerator AppearAndTroll()
    {
        SetAppearOffset();
        
        bool isJumpScare = Random.value < jumpScareChance;
        
        if (isJumpScare)
        {
            yield return StartCoroutine(JumpScareAppear());
        }
        else
        {
            yield return StartCoroutine(NormalAppear());
        }
    }

    void SetAppearOffset()
    {
        int type = Random.Range(0, 5);
        
        switch (type)
        {
            case 0: // Front
                appearOffset = Vector3.zero;
                break;
            case 1: // Left
                appearOffset = new Vector3(-0.8f, 0f, 0f);
                break;
            case 2: // Right
                appearOffset = new Vector3(0.8f, 0f, 0f);
                break;
            case 3: // Above
                appearOffset = new Vector3(0f, 0.6f, 0f);
                break;
            case 4: // Below
                appearOffset = new Vector3(0f, -0.4f, 0f);
                break;
        }
    }

    IEnumerator NormalAppear()
    {
        SetInitialPositionAndRotation();
        isFollowing = true;
        SetVisible(true);
        
        yield return new WaitForSeconds(appearDuration);
        
        yield return StartCoroutine(DisappearAnimation());
    }

    IEnumerator JumpScareAppear()
    {
        float originalDistance = distanceFromCamera;
        distanceFromCamera = 0.8f;
        
        SetInitialPositionAndRotation();
        isFollowing = true;
        SetVisible(true);
        
        yield return new WaitForSeconds(0.5f);
        
        distanceFromCamera = originalDistance;
        
        yield return new WaitForSeconds(0.3f);
        
        yield return StartCoroutine(DisappearAnimation());
    }

    IEnumerator DisappearAnimation()
    {
        isFollowing = false;
        
        int disappearType = Random.Range(0, 3);
        float duration = 0.5f;
        float elapsed = 0f;
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        
        switch (disappearType)
        {
            case 0:
                endPos += Vector3.down * 2f;
                break;
            case 1:
                endPos += -cameraTransform.right * 3f;
                break;
            case 2:
                endPos += cameraTransform.right * 3f;
                break;
        }
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t * t);
            yield return null;
        }
        
        SetVisible(false);
    }

    void SetVisible(bool visible)
    {
        isVisible = visible;
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = visible;
        }
    }

    public void TriggerAppearance(bool jumpScare = false)
    {
        if (!isCameraFound) return;
        
        StopAllCoroutines();
        
        if (jumpScare)
        {
            StartCoroutine(JumpScareAppear());
        }
        else
        {
            StartCoroutine(NormalAppear());
        }
        
        StartCoroutine(TrollRoutine());
    }

    public void SetOnTriggerAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
        StartCoroutine(ChangeToIdle());
    }

    private IEnumerator ChangeToIdle()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("idle");
    }
}