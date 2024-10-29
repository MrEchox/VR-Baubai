using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRCrouchController : MonoBehaviour
{
    public CharacterController characterController; 
    public Transform cameraTransform; 
    public float crouchHeight = 0.5f; 
    public float crouchSpeed = 5f; 
    public InputActionReference crouchActionReference; 

    private float originalHeight; 
    private Coroutine crouchCoroutine; 
    private bool isCrouching = false; 

    private void Awake()
    {
        
        crouchActionReference.action.Enable();
    }

    private void OnEnable()
    {
        
        crouchActionReference.action.performed += ToggleCrouch;
    }

    private void OnDisable()
    {
        
        crouchActionReference.action.performed -= ToggleCrouch;
    }

    private void Start()
    {
        
        originalHeight = characterController.height;
    }

    private void ToggleCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouch action performed!");
        isCrouching = !isCrouching;

        if (crouchCoroutine != null)
        {
            StopCoroutine(crouchCoroutine);
        }

        crouchCoroutine = StartCoroutine(SmoothTransition());
    }

    private IEnumerator SmoothTransition()
    {
        
        float targetHeight = isCrouching ? originalHeight - crouchHeight : originalHeight;
        float elapsedTime = 0f;
        float startHeight = characterController.height;

        
        float startCameraY = cameraTransform.localPosition.y;

        while (elapsedTime < 1f)
        {
            
            characterController.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime);
            characterController.center = new Vector3(characterController.center.x, characterController.height / 2, characterController.center.z);

            
            float targetCameraY = isCrouching ? crouchHeight : 0;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, Mathf.Lerp(startCameraY, targetCameraY, elapsedTime), cameraTransform.localPosition.z);

            elapsedTime += Time.deltaTime * crouchSpeed;
            yield return null; 
        }

        
        characterController.height = targetHeight;
        characterController.center = new Vector3(characterController.center.x, characterController.height / 2, characterController.center.z);

        
        cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, (isCrouching ? crouchHeight : 0), cameraTransform.localPosition.z);

        Debug.Log($"Final height set to: {characterController.height}");
    }

    private void Update()
    {
        // Lock the CharacterController height if crouching
        if (isCrouching)
        {
            characterController.height = originalHeight - crouchHeight;
            characterController.center = new Vector3(characterController.center.x, characterController.height / 2, characterController.center.z);
        }
    }
}
