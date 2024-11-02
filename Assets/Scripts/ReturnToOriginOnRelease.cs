using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnToOriginOnRelease : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public XRInteractionManager interactionManager;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public Transform generator;
    public float maxUngrabDistance = 4.0f; // Maximum distance before ungrabbing
    public float smoothSpeed = 10.0f; // Return speeed

    private Rigidbody handleRigidBody;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        handleRigidBody = GetComponent<Rigidbody>();

        if (grabInteractable != null)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            grabInteractable.selectExited.AddListener(OnObjectUngrabbed);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnObjectUngrabbed);
        }
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            Vector3 direction = transform.position - generator.position;
            if (direction.magnitude > maxUngrabDistance)
            {
                var interactor = grabInteractable.interactorsSelecting[0]; // Get the first interactor
                interactionManager.SelectExit(interactor, grabInteractable);

            }
        }
    }
    private void OnObjectUngrabbed(SelectExitEventArgs args)
    {
        StartCoroutine(ReturnToOrigin());
    }

    private IEnumerator ReturnToOrigin()
    {
        handleRigidBody.isKinematic = true; // Temp disabling of physics

        float elapsedTime = 0f;
        float duration = 0.4f; // Duration of the return
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            // Interpolate position and rotation back to original
            transform.position = Vector3.Lerp(startPosition, originalPosition, (elapsedTime / duration));
            transform.rotation = Quaternion.Slerp(startRotation, originalRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and rotation are set correctly
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        handleRigidBody.isKinematic = false; // Re-enable physics
    }
}
