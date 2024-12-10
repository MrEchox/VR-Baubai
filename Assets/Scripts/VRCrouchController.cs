using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchMechanic : MonoBehaviour
{
    public InputActionProperty crouchAction; 
    public float standingHeight = 1.8f;
    public float crouchHeight = 1.0f;
    public float crouchSpeed = 5f;

    private Transform cameraOffset;  

    void Start()
    {
        
        cameraOffset = transform.Find("Camera Offset");

       
    }

    void Update()
    {
        
        bool isCrouching = crouchAction.action.IsPressed();

        float targetHeight = isCrouching ? crouchHeight : standingHeight;

        Vector3 targetPosition = cameraOffset.localPosition;
        targetPosition.y = targetHeight;

        cameraOffset.localPosition = Vector3.Lerp(
            cameraOffset.localPosition,
            targetPosition,
            Time.deltaTime * crouchSpeed
        );
    }
}
